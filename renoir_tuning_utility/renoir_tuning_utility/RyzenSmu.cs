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
        [Serializable]
        [StructLayout(LayoutKind.Explicit)]
        private struct RenoirMobilePT
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

        public enum CounterIndex : uint
        {
            STAPM_LIMIT = 0x000,
            STAPM_POWER = 0x004,
            FAST_LIMIT = 0x008,
            CURRENT_POWER = 0x00C,
            SLOW_LIMIT = 0x010,
            SLOW_POWER = 0x014,
            EDC_LIMIT = 0x020,
            EDC_USED = 0x024,
            SOC_EDC_LIMIT = 0x028,
            SOC_EDC_USED = 0x02C,
            TDC_LIMIT = 0x030,
            TDC_USED = 0x034,
            SOC_TDC_LIMIT = 0x038,
            SOC_TDC_USED = 0x03C,
            THERMAL_JUNCTION = 0x040,
            CURRENT_TEMP = 0x044,
            THERMAL_JUNCTION_2 = 0x048,
            CORE_TEMP = 0x04C,
            THERMAL_JUNCTION_3 = 0x050,
            CORE_TEMP_2 = 0x054,
            SVI_PEAK_CORE = 0x070,
            SVI_CORE = 0x074,
            IF_FREQUENCY = 0x144,
            IF_FREQUENCY_2 = 0x148,
            UNCORE_FREQUENCY = 0x154,
            UNCORE_FREQUENCY_2 = 0x158,
            MCLK_FREQUENCY = 0x164,
            MCLK_FREQUENCY_2 = 0x168,
            SVI_SOC = 0x174,
            CLDO_VDDG = 0x178,
            MAX_CORE_CLK = 0x480,
            MIN_CORE_CLK = 0x4A0,
            C_STATES = 0x4E0,
            GATED_CLOCK = 0x520,
            IGPU_FREQUENCY = 0x5B4,
            IF_FREQUENCY_3 = 0x5CC,
            UNCORE_FREQUENCY_3 = 0x5D0,
            MCLK_FREQUENCY_3 = 0x5D4,
            IF_FREQUENCY_4 = 0x5F4,
            UNCORE_FREQUENCY_4 = 0x5F8,
            MCLK_FREQUENCY_4    = 0x5FC,
            GPU_SOC_CLOCK = 0x65C,
            BAD
        }

        private static readonly Dictionary<RyzenSmu.CounterIndex, String> CounterName = new Dictionary<RyzenSmu.CounterIndex, string>()
        {
            { RyzenSmu.CounterIndex.STAPM_LIMIT, "stapm_limit" },
            { RyzenSmu.CounterIndex.STAPM_POWER, "stapm_power" },
            { RyzenSmu.CounterIndex.FAST_LIMIT, "fast_limit" },
            { RyzenSmu.CounterIndex.CURRENT_POWER, "current_power" },
            { RyzenSmu.CounterIndex.SLOW_LIMIT, "slow_limit" },
            { RyzenSmu.CounterIndex.SLOW_POWER, "slow_power" },
            { RyzenSmu.CounterIndex.EDC_LIMIT, "edc_limit" },
            { RyzenSmu.CounterIndex.EDC_USED, "edc_used" },
            { RyzenSmu.CounterIndex.SOC_EDC_LIMIT, "soc_edc_limit" },
            { RyzenSmu.CounterIndex.SOC_EDC_USED, "soc_edc_used" },
            { RyzenSmu.CounterIndex.TDC_LIMIT, "tdc_limit" },
            { RyzenSmu.CounterIndex.TDC_USED, "tdc_used" },
            { RyzenSmu.CounterIndex.SOC_TDC_LIMIT, "soc_tdc_limit" },
            { RyzenSmu.CounterIndex.SOC_TDC_USED, "soc_tdc_used" },
            { RyzenSmu.CounterIndex.THERMAL_JUNCTION, "thermal_junction" },
            { RyzenSmu.CounterIndex.CURRENT_TEMP, "current_temp" },
            { RyzenSmu.CounterIndex.THERMAL_JUNCTION_2, "thermal_junction_2" },
            { RyzenSmu.CounterIndex.CORE_TEMP, "core_temp" },
            { RyzenSmu.CounterIndex.THERMAL_JUNCTION_3, "thermal_junction_3" },
            { RyzenSmu.CounterIndex.CORE_TEMP_2, "core_temp_2" },
            { RyzenSmu.CounterIndex.SVI_PEAK_CORE, "svi_peak_core" },
            { RyzenSmu.CounterIndex.SVI_CORE, "svi_core" },
            { RyzenSmu.CounterIndex.IF_FREQUENCY, "if_frequency" },
            { RyzenSmu.CounterIndex.IF_FREQUENCY_2, "if_frequency_2" },
            { RyzenSmu.CounterIndex.UNCORE_FREQUENCY, "uncore_frequency" },
            { RyzenSmu.CounterIndex.UNCORE_FREQUENCY_2, "uncore_frequency_2" },
            { RyzenSmu.CounterIndex.MCLK_FREQUENCY, "mclk_frequency" },
            { RyzenSmu.CounterIndex.MCLK_FREQUENCY_2, "mclk_frequency_2" },
            { RyzenSmu.CounterIndex.SVI_SOC, "svi_soc" },
            { RyzenSmu.CounterIndex.CLDO_VDDG, "cldo_vddg" },
            { RyzenSmu.CounterIndex.MAX_CORE_CLK, "max_core_clk" },
            { RyzenSmu.CounterIndex.MIN_CORE_CLK, "min_core_clk" },
            { RyzenSmu.CounterIndex.C_STATES, "c_states" },
            { RyzenSmu.CounterIndex.GATED_CLOCK, "gated_clock" },
            { RyzenSmu.CounterIndex.IGPU_FREQUENCY, "igpu_frequency" },
            { RyzenSmu.CounterIndex.IF_FREQUENCY_3, "if_frequency_3" },
            { RyzenSmu.CounterIndex.UNCORE_FREQUENCY_3, "uncore_frequency_3" },
            { RyzenSmu.CounterIndex.MCLK_FREQUENCY_3, "mclk_frequency_3" },
            { RyzenSmu.CounterIndex.IF_FREQUENCY_4, "if_frequency_4" },
            { RyzenSmu.CounterIndex.UNCORE_FREQUENCY_4, "uncore_frequency_4" },
            { RyzenSmu.CounterIndex.MCLK_FREQUENCY_4, "mclk_frequency_4" },
            { RyzenSmu.CounterIndex.GPU_SOC_CLOCK, "gpu_soc_clock" },
            { RyzenSmu.CounterIndex.BAD, "" }
        };
    }
}
