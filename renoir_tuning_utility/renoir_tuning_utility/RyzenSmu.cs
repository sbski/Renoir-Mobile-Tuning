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
using System.Windows.Forms;

[assembly: CLSCompliant(false)]


namespace RyzenSmu
{


    public class PerfomanceCounter
    {
        public String Description { get; protected set; }
        public String Units { get; protected set; }

        public uint Index { get; protected set; }
        public uint Offset { get; protected set; }

        public float Value { get; protected set; }

        public PerfomanceCounter(String CounterDescription, String CounterUnits, uint PMTableOffset)
        {
            Description = CounterDescription;
            Units = CounterUnits;
            Index = PMTableOffset / 4;
            Offset = PMTableOffset;

        }
        public float Update(uint MemoryBaseAddress)
        {
            Value = Smu.ReadFloat(MemoryBaseAddress, Offset);
            return Value;
        }
    }
    public class PowerMonitoringTable
    {
        public uint MemoryBaseAddress { get; protected set; }
        public uint TableSize { get; protected set; }
        

        public PerfomanceCounter[] PerfomanceCounter { get; protected set; }


        PowerMonitoringTable()
        {
            TableSize = 1000;
            MemoryBaseAddress = 0;
        }

        bool UpdateTable()
        {
            if (MemoryBaseAddress == 0)
                return false;

            //Updates the value of each counter
            for(int i = 0; i < PerfomanceCounter.Length; i++)
            {
                PerfomanceCounter[i].Update(MemoryBaseAddress);
            }

            return true;
        }

        public void SetBaseAddress(uint MemoryAddress)
        {
            MemoryBaseAddress = MemoryAddress;
        }


        public class RenoirMobilePT:PowerMonitoringTable
        {
            public RenoirMobilePT()
            {
                TableSize = 410;
                PerfomanceCounter = new PerfomanceCounter[41];

                PerfomanceCounter[0] = new PerfomanceCounter("Stapm Limit", "Watts", 0x000);
                PerfomanceCounter[1] = new PerfomanceCounter("Stapm Power", "Watts", 0x004);
                PerfomanceCounter[2] = new PerfomanceCounter("Fast Limit", "Watts", 0x008);
                PerfomanceCounter[3] = new PerfomanceCounter("Current Power", "Watts", 0x00C);
                PerfomanceCounter[4] = new PerfomanceCounter("Slow Limit", "Watts", 0x010);
                PerfomanceCounter[5] = new PerfomanceCounter("Slow Power", "Watts", 0x014);
                PerfomanceCounter[6] = new PerfomanceCounter("Edc Limit", "Amps", 0x020);
                PerfomanceCounter[7] = new PerfomanceCounter("Edc Used", "Amps", 0x024);
                PerfomanceCounter[8] = new PerfomanceCounter("Soc Edc Limit", "Amps", 0x028);
                PerfomanceCounter[9] = new PerfomanceCounter("Soc Edc Used", "Amps", 0x02C);
                PerfomanceCounter[10] = new PerfomanceCounter("Tdc Limit", "Amps", 0x030);
                PerfomanceCounter[11] = new PerfomanceCounter("Tdc Used", "Amps", 0x034);
                PerfomanceCounter[12] = new PerfomanceCounter("Soc Tdc Limit", "Amps", 0x038);
                PerfomanceCounter[13] = new PerfomanceCounter("Soc Tdc Used", "Amps", 0x03C);
                PerfomanceCounter[14] = new PerfomanceCounter("Thermal Junction", "C", 0x040);
                PerfomanceCounter[15] = new PerfomanceCounter("Current Temp", "C", 0x044);
                PerfomanceCounter[16] = new PerfomanceCounter("Thermal Junction 2", "C", 0x048);
                PerfomanceCounter[17] = new PerfomanceCounter("Core Temp", "C", 0x04C);
                PerfomanceCounter[18] = new PerfomanceCounter("Thermal Junction 3", "C", 0x050);
                PerfomanceCounter[19] = new PerfomanceCounter("Core Temp 2", "C", 0x054);
                PerfomanceCounter[20] = new PerfomanceCounter("Svi Peak Core", "Volts", 0x070);
                PerfomanceCounter[21] = new PerfomanceCounter("Svi Core", "Volts", 0x074);
                PerfomanceCounter[22] = new PerfomanceCounter("If Frequency", "MHz", 0x144);
                PerfomanceCounter[23] = new PerfomanceCounter("If Frequency 2", "MHz", 0x148);
                PerfomanceCounter[24] = new PerfomanceCounter("Uncore Frequency", "MHz", 0x154);
                PerfomanceCounter[25] = new PerfomanceCounter("Uncore Frequency 2", "MHz", 0x158);
                PerfomanceCounter[26] = new PerfomanceCounter("Mclk Frequency", "MHz", 0x164);
                PerfomanceCounter[27] = new PerfomanceCounter("Mclk Frequency 2", "MHz", 0x168);
                PerfomanceCounter[28] = new PerfomanceCounter("Svi Soc", "Volts", 0x174);
                PerfomanceCounter[29] = new PerfomanceCounter("Cldo Vddg", "Volts", 0x178);
                PerfomanceCounter[30] = new PerfomanceCounter("Max Core Clk", "MHz", 0x480);
                PerfomanceCounter[31] = new PerfomanceCounter("Min Core Clk", "MHz", 0x4A0);
                PerfomanceCounter[32] = new PerfomanceCounter("C States", "Percent", 0x4E0);
                PerfomanceCounter[33] = new PerfomanceCounter("Gated Clock", "Percent", 0x520);
                PerfomanceCounter[34] = new PerfomanceCounter("Igpu Frequency", "MHz", 0x5B4);
                PerfomanceCounter[35] = new PerfomanceCounter("If Frequency", "MHz", 0x5CC);
                PerfomanceCounter[36] = new PerfomanceCounter("Uncore Frequency", "MHz", 0x5D0);
                PerfomanceCounter[37] = new PerfomanceCounter("Mclk Frequency", "MHz", 0x5D4);
                PerfomanceCounter[38] = new PerfomanceCounter("If Frequency", "MHz", 0x5F4);
                PerfomanceCounter[39] = new PerfomanceCounter("Uncore Frequency", "MHz", 0x5F8);
                PerfomanceCounter[40] = new PerfomanceCounter("Mclk Frequency", "MHz", 0x5FC);
                PerfomanceCounter[41] = new PerfomanceCounter("Gpu Soc Clock", "MHz", 0x65C);
            }
        }

        
        /*
        [Serializable]
        [StructLayout(LayoutKind.Explicit)]
        public struct RenoirMobilePT
        {
            [FieldOffset(0x000)] public float stapm_limit;
            [FieldOffset(0x004)] public float stapm_power;
            [FieldOffset(0x008)] public float fast_limit;
            [FieldOffset(0x00C)] public float current_power;
            [FieldOffset(0x010)] public float slow_limit;
            [FieldOffset(0x014)] public float slow_power;
            [FieldOffset(0x020)] public float edc_limit;
            [FieldOffset(0x024)] public float edc_used;
            [FieldOffset(0x028)] public float soc_edc_limit;
            [FieldOffset(0x02C)] public float soc_edc_used;
            [FieldOffset(0x030)] public float tdc_limit;
            [FieldOffset(0x034)] public float tdc_used;
            [FieldOffset(0x038)] public float soc_tdc_limit;
            [FieldOffset(0x03C)] public float soc_tdc_used;
            [FieldOffset(0x040)] public float thermal_junction;
            [FieldOffset(0x044)] public float current_temp;
            [FieldOffset(0x048)] public float thermal_junction_2;
            [FieldOffset(0x04C)] public float core_temp;
            [FieldOffset(0x050)] public float thermal_junction_3;
            [FieldOffset(0x054)] public float core_temp_2;
            [FieldOffset(0x070)] public float svi_peak_core;
            [FieldOffset(0x074)] public float svi_core;
            [FieldOffset(0x144)] public float if_frequency;
            [FieldOffset(0x148)] public float if_frequency_2;
            [FieldOffset(0x154)] public float uncore_frequency;
            [FieldOffset(0x158)] public float uncore_frequency_2;
            [FieldOffset(0x164)] public float mclk_frequency;
            [FieldOffset(0x168)] public float mclk_frequency_2;
            [FieldOffset(0x174)] public float svi_soc;
            [FieldOffset(0x178)] public float cldo_vddg;
            [FieldOffset(0x480)] public float max_core_clk;
            [FieldOffset(0x4A0)] public float min_core_clk;
            [FieldOffset(0x4E0)] public float c_states;
            [FieldOffset(0x520)] public float gated_clock;
            [FieldOffset(0x5B4)] public float igpu_frequency;
            [FieldOffset(0x5CC)] public float if_frequenc_3;
            [FieldOffset(0x5D0)] public float uncore_frequency_3;
            [FieldOffset(0x5D4)] public float mclk_frequency_3;
            [FieldOffset(0x5F4)] public float if_frequency_4;
            [FieldOffset(0x5F8)] public float uncore_frequency_4;
            [FieldOffset(0x5FC)] public float mclk_frequency_4;
            [FieldOffset(0x65C)] public float gpu_soc_clock;
        };
        */

    }
    class Smu
    {
        

        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysLong(UIntPtr memAddress, out uint Data);

        public static float ReadFloat(uint Address, uint Offset)
        {
            uint Data = 0;
            try
            {
                GetPhysLong((UIntPtr)(Address + Offset * 4), out Data);
            }
            catch(Exception e)
            {
                String ExeptionMSG = $"Error Reading Address 0x{Address:X8} + 0x{Offset:X4}";
                MessageBox.Show(ExeptionMSG);
            }

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

        private static readonly Dictionary<Smu.Status, String> status = new Dictionary<Smu.Status, string>()
        {
            { Smu.Status.BAD, "BAD" },
            { Smu.Status.OK, "OK" },
            { Smu.Status.FAILED, "Failed" },
            { Smu.Status.UNKNOWN_CMD, "Unknown Command" },
            { Smu.Status.CMD_REJECTED_PREREQ, "CMD Rejected Prereq" },
            { Smu.Status.CMD_REJECTED_BUSY, "CMD Rejected Busy" }
        };


        
        Ols RyzenAccesss;
        

        public Smu(bool EnableDebug)
        {
            ShowDebug = EnableDebug;
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
                    if (ShowDebug)
                    {
                        MessageBox.Show("Ols Dll is OK.", "Ols.OlsDllStatus:");
                    }
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    MessageBox.Show("WinRing OLS_DRIVER_NOT_LOADED", "Ols.OlsDllStatus:");
                    throw new ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");

                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    MessageBox.Show("WinRing OLS_UNSUPPORTED_PLATFORM", "Ols.OlsDllStatus:");
                    throw new ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");

                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    MessageBox.Show("WinRing OLS_DLL_DRIVER_NOT_FOUND", "Ols.OlsDllStatus:");
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");

                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    MessageBox.Show("WinRing OLS_DLL_DRIVER_UNLOADED", "Ols.OlsDllStatus:");
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");

                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    MessageBox.Show("WinRing DRIVER_NOT_LOADED_ON_NETWORK", "Ols.OlsDllStatus:");
                    throw new ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");

                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    MessageBox.Show("WinRing OLS_DLL_UNKNOWN_ERROR", "Ols.OlsDllStatus:");
                    throw new ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }
            WriteReg(PSMU_ADDR_RSP, 0x1);
        }

        public void Initialize()
        {
            
            RyzenAccesss.InitializeOls();

            // Check WinRing0 status
            switch (RyzenAccesss.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    if (ShowDebug)
                    {
                        MessageBox.Show("Ols is OK.", "Ols.Status:");
                        ShowDebug = false;
                    }
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    MessageBox.Show("WinRing Status: DLL_NOT_FOUND", "Ols.Status:");
                    throw new ApplicationException("WinRing DLL_NOT_FOUND");
                    break;
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    MessageBox.Show("WinRing Status: DLL_INCORRECT_VERSION", "Ols.Status:");
                    throw new ApplicationException("WinRing DLL_INCORRECT_VERSION");
                    break;
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    MessageBox.Show("WinRing Status: DLL_INITIALIZE_ERROR", "Ols.Status:");
                    throw new ApplicationException("WinRing DLL_INITIALIZE_ERROR");
                    break;
                default:
                    break;


            }
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

        public bool ShowDebug { get; set; }

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


        //Code by I.nfraR.ed to get the CpuID
        private string GetStringPart(uint val)
        {
            return val != 0 ? Convert.ToChar(val).ToString() : "";
        }

        private string IntToStr(uint val)
        {
            uint part1 = val & 0xff;
            uint part2 = val >> 8 & 0xff;
            uint part3 = val >> 16 & 0xff;
            uint part4 = val >> 24 & 0xff;

            return string.Format("{0}{1}{2}{3}", GetStringPart(part1), GetStringPart(part2), GetStringPart(part3), GetStringPart(part4));
        }

        public string GetCpuName()
        {
            string model = "";
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;

            if (RyzenAccesss.Cpuid(0x80000002, ref eax, ref ebx, ref ecx, ref edx) == 1)
                model = model + IntToStr(eax) + IntToStr(ebx) + IntToStr(ecx) + IntToStr(edx);

            if (RyzenAccesss.Cpuid(0x80000003, ref eax, ref ebx, ref ecx, ref edx) == 1)
                model = model + IntToStr(eax) + IntToStr(ebx) + IntToStr(ecx) + IntToStr(edx);

            if (RyzenAccesss.Cpuid(0x80000004, ref eax, ref ebx, ref ecx, ref edx) == 1)
                model = model + IntToStr(eax) + IntToStr(ebx) + IntToStr(ecx) + IntToStr(edx);

            return model.Trim();
        }

        /*
        
        */

        
    }
}
