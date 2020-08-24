using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenLibSys;
using System.Reflection;

[assembly: CLSCompliant(false)]


namespace RyzenSmu
{
    class RyzenSmu
    {
        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysLong(UIntPtr memAddress, out uint Data);

        public float ReadFloat(uint Address, uint Offset)
        {
            uint Data = 0;
            GetPhysLong((UIntPtr)(Address + Offset * 4), out Data);

            byte[] bytes = new byte[4];
            bytes = BitConverter.GetBytes(Data);

            float PmData = BitConverter.ToSingle(bytes, 0);
            //Console.WriteLine($"0x{Address + Offset * 4,8:X8} | {PmData:F}");
            return PmData;
        }


        public enum Status : int
        {
            BAD = 0x0,
            OK = 0x1,
            FAILED = 0xFF,
            UNKNOWN_CMD = 0xFE,
            CMD_REJECTED_PREREQ = 0xFD,
            CMD_REJECTED_BUSY = 0xFC
        }

        private static readonly Dictionary<RyzenSmu.Status, String> status = new Dictionary<RyzenSmu.Status, string>()
        {
            { RyzenSmu.Status.BAD, "BAD" },
            { RyzenSmu.Status.OK, "OK" },
            { RyzenSmu.Status.FAILED, "Failed" },
            { RyzenSmu.Status.UNKNOWN_CMD, "Unknown Command" },
            { RyzenSmu.Status.CMD_REJECTED_PREREQ, "CMD Rejected Prereq" },
            { RyzenSmu.Status.CMD_REJECTED_BUSY, "CMD Rejected Busy" }
        };

        Ols RyzenAccesss;
        public RyzenSmu()
        {
            RyzenAccesss = new Ols();
            //RyzenAccesss.InitializeOls();

            SMU_PCI_ADDR = 0x00000000;
            SMU_OFFSET_ADDR = 0xB8;
            SMU_OFFSET_DATA = 0xBC;

            MP1_ADDR_MSG = 0x03B10528;
            MP1_ADDR_RSP = 0x03B10564;
            MP1_ADDR_ARG = 0x03B10998;

            PSMU_ADDR_MSG = 0x03B10A20;
            PSMU_ADDR_RSP = 0x03B10A80;
            PSMU_ADDR_ARG = 0x03B10A88;

            // Check WinRing0 status
            switch (RyzenAccesss.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }
            WriteReg(PSMU_ADDR_RSP, 0x1);
        }

        public void Initialize()
        {
            RyzenAccesss.InitializeOls();
        }

        public void Deinitialize()
        {
            RyzenAccesss.DeinitializeOls();
        }



        public uint SMU_PCI_ADDR { get; protected set; }
        public uint SMU_OFFSET_ADDR { get; protected set; }
        public uint SMU_OFFSET_DATA { get; protected set; }

        public uint MP1_ADDR_MSG { get; protected set; }
        public uint MP1_ADDR_RSP { get; protected set; }
        public uint MP1_ADDR_ARG { get; protected set; }

        public uint PSMU_ADDR_MSG { get; protected set; }
        public uint PSMU_ADDR_RSP { get; protected set; }
        public uint PSMU_ADDR_ARG { get; protected set; }
        public uint[] Args { get; set; }

        public Status SendMp1(uint message, ref uint[] arguments)
        {
            return SendMsg(MP1_ADDR_MSG, MP1_ADDR_RSP, MP1_ADDR_ARG, message, ref arguments);
        }

        public Status SendPsmu(uint message, ref uint[] arguments)
        {
            return SendMsg(PSMU_ADDR_MSG, PSMU_ADDR_RSP, PSMU_ADDR_ARG, message, ref arguments);
        }

        public Status SendMsg(uint SMU_ADDR_MSG, uint SMU_ADDR_RSP, uint SMU_ADDR_ARG, uint message, ref uint[] arguments)
        {
            //Wait for the Rsp register to not be 0;
            Wait4Rsp(SMU_ADDR_RSP);

            uint status = 0;
            //Clear the REP register
            WriteReg(SMU_ADDR_RSP, 0x0);

            //Write the ARGs
            for (uint i = 0; i < 6; i++)
            {
                if (i >= arguments.Length)
                {
                    WriteReg((SMU_ADDR_ARG + (i * (uint)4)), (uint)0);
                }
                else
                {
                    WriteReg((SMU_ADDR_ARG + (i * (uint)4)), arguments[i]);
                }
            }

            //Write the MSG
            WriteReg(SMU_ADDR_MSG, message);

            //Poll the REP until it changed, means the request is proceeded
            Wait4Rsp(SMU_ADDR_RSP);

            //Thread.Sleep(100);
            //Read back ARGs
            for (uint i = 0; i < 6; i++)
            {

                ReadReg((SMU_ADDR_ARG + (i * (uint)4)), ref arguments[i]);
                //Console.WriteLine($"Arg{(i + 1):D}: 0x{arguments[i],8:X8}");
            }


            //Check the REP if the request proceeded sucessfuly 
            ReadReg(SMU_ADDR_RSP, ref status);

            //WriteReg(SMU_ADDR_RSP, 0x0);

            //Return the status of the Request
            return (Status)status;
        }


        private bool WriteReg(uint addr, uint data)
        {
            if (RyzenAccesss.WritePciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_ADDR, addr) == 1)
            {
                return RyzenAccesss.WritePciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_DATA, data) == 1;
            }
            return false;
        }

        private bool ReadReg(uint addr, ref uint data)
        {
            if (RyzenAccesss.WritePciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_ADDR, addr) == 1)
            {
                return RyzenAccesss.ReadPciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_DATA, ref data) == 1;
            }
            return false;
        }

        private uint ReadDword(uint value)
        {
            RyzenAccesss.WritePciConfigDword(SMU_PCI_ADDR, (byte)SMU_OFFSET_ADDR, value);
            return RyzenAccesss.ReadPciConfigDword(SMU_PCI_ADDR, (byte)SMU_OFFSET_DATA);
        }


        private bool Wait4Rsp(uint SMU_ADDR_RSP)
        {
            bool res = false;
            ushort timeout = 1000;
            uint data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
                res = ReadReg(SMU_ADDR_RSP, ref data);
                Thread.Sleep(1);
            }

            //Console.WriteLine($"Time{(timeout):D}: 0x{data,8:X8}");


            if (timeout == 0 || data != 1) res = false;
            //Console.WriteLine($"Res{(res):D}");


            return res;
        }
    }
}
