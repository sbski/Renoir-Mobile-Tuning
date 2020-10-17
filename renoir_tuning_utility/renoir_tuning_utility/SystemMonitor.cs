using Microsoft.Win32;
using Newtonsoft.Json;
using PowerSettings;
using RyzenSmu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace renoir_tuning_utility
{



    public partial class SystemMonitor : Form
    {
        UInt32 PMTableVersion;
        private readonly uint[] OffsetTable = {
            0x0,
            0x4,
            0x8,
            0xC,
            0x10,
            0x14,
            0x18,
            0x1C,
            0x20,
            0x24,
            0x28,
            0x2C,
            0x30,
            0x34,
            0x38,
            0x3C,
            0x40,
            0x44,
            0x48,
            0x4C,
            0x50,
            0x54,
            0x58,
            0x5C,
            0x60,
            0x64,
            0x68,
            0x6C,
            0x70,
            0x74,
            0x78,
            0x7C,
            0x80,
            0x84,
            0x88,
            0x8C,
            0x90,
            0x94,
            0x98,
            0x9C,
            0xA0,
            0xA4,
            0xA8,
            0xAC,
            0xB0,
            0xB4,
            0xB8,
            0xBC,
            0xC0,
            0xC4,
            0xC8,
            0xCC,
            0xD0,
            0xD4,
            0xD8,
            0xDC,
            0xE0,
            0xE4,
            0xE8,
            0xEC,
            0xF0,
            0xF4,
            0xF8,
            0xFC,
            0x100,
            0x104,
            0x108,
            0x10C,
            0x110,
            0x114,
            0x118,
            0x11C,
            0x120,
            0x124,
            0x128,
            0x12C,
            0x130,
            0x134,
            0x138,
            0x13C,
            0x140,
            0x144,
            0x148,
            0x14C,
            0x150,
            0x154,
            0x158,
            0x15C,
            0x160,
            0x164,
            0x168,
            0x16C,
            0x170,
            0x174,
            0x178,
            0x17C,
            0x180,
            0x184,
            0x188,
            0x18C,
            0x190,
            0x194,
            0x198,
            0x19C,
            0x1A0,
            0x1A4,
            0x1A8,
            0x1AC,
            0x1B0,
            0x1B4,
            0x1B8,
            0x1BC,
            0x1C0,
            0x1C4,
            0x1C8,
            0x1CC,
            0x1D0,
            0x1D4,
            0x1D8,
            0x1DC,
            0x1E0,
            0x1E4,
            0x1E8,
            0x1EC,
            0x1F0,
            0x1F4,
            0x1F8,
            0x1FC,
            0x200,
            0x204,
            0x208,
            0x20C,
            0x210,
            0x214,
            0x218,
            0x21C,
            0x220,
            0x224,
            0x228,
            0x22C,
            0x230,
            0x234,
            0x238,
            0x23C,
            0x240,
            0x244,
            0x248,
            0x24C,
            0x250,
            0x254,
            0x258,
            0x25C,
            0x260,
            0x264,
            0x268,
            0x26C,
            0x270,
            0x274,
            0x278,
            0x27C,
            0x280,
            0x284,
            0x288,
            0x28C,
            0x290,
            0x294,
            0x298,
            0x29C,
            0x2A0,
            0x2A4,
            0x2A8,
            0x2AC,
            0x2B0,
            0x2B4,
            0x2B8,
            0x2BC,
            0x2C0,
            0x2C4,
            0x2C8,
            0x2CC,
            0x2D0,
            0x2D4,
            0x2D8,
            0x2DC,
            0x2E0,
            0x2E4,
            0x2E8,
            0x2EC,
            0x2F0,
            0x2F4,
            0x2F8,
            0x2FC,
            0x300,
            0x304,
            0x308,
            0x30C,
            0x310,
            0x314,
            0x318,
            0x31C,
            0x320,
            0x324,
            0x328,
            0x32C,
            0x330,
            0x334,
            0x338,
            0x33C,
            0x340,
            0x344,
            0x348,
            0x34C,
            0x350,
            0x354,
            0x358,
            0x35C,
            0x360,
            0x364,
            0x368,
            0x36C,
            0x370,
            0x374,
            0x378,
            0x37C,
            0x380,
            0x384,
            0x388,
            0x38C,
            0x390,
            0x394,
            0x398,
            0x39C,
            0x3A0,
            0x3A4,
            0x3A8,
            0x3AC,
            0x3B0,
            0x3B4,
            0x3B8,
            0x3BC,
            0x3C0,
            0x3C4,
            0x3C8,
            0x3CC,
            0x3D0,
            0x3D4,
            0x3D8,
            0x3DC,
            0x3E0,
            0x3E4,
            0x3E8,
            0x3EC,
            0x3F0,
            0x3F4,
            0x3F8,
            0x3FC,
            0x400,
            0x404,
            0x408,
            0x40C,
            0x410,
            0x414,
            0x418,
            0x41C,
            0x420,
            0x424,
            0x428,
            0x42C,
            0x430,
            0x434,
            0x438,
            0x43C,
            0x440,
            0x444,
            0x448,
            0x44C,
            0x450,
            0x454,
            0x458,
            0x45C,
            0x460,
            0x464,
            0x468,
            0x46C,
            0x470,
            0x474,
            0x478,
            0x47C,
            0x480,
            0x484,
            0x488,
            0x48C,
            0x490,
            0x494,
            0x498,
            0x49C,
            0x4A0,
            0x4A4,
            0x4A8,
            0x4AC,
            0x4B0,
            0x4B4,
            0x4B8,
            0x4BC,
            0x4C0,
            0x4C4,
            0x4C8,
            0x4CC,
            0x4D0,
            0x4D4,
            0x4D8,
            0x4DC,
            0x4E0,
            0x4E4,
            0x4E8,
            0x4EC,
            0x4F0,
            0x4F4,
            0x4F8,
            0x4FC,
            0x500,
            0x504,
            0x508,
            0x50C,
            0x510,
            0x514,
            0x518,
            0x51C,
            0x520,
            0x524,
            0x528,
            0x52C,
            0x530,
            0x534,
            0x538,
            0x53C,
            0x540,
            0x544,
            0x548,
            0x54C,
            0x550,
            0x554,
            0x558,
            0x55C,
            0x560,
            0x564,
            0x568,
            0x56C,
            0x570,
            0x574,
            0x578,
            0x57C,
            0x580,
            0x584,
            0x588,
            0x58C,
            0x590,
            0x594,
            0x598,
            0x59C,
            0x5A0,
            0x5A4,
            0x5A8,
            0x5AC,
            0x5B0,
            0x5B4,
            0x5B8,
            0x5BC,
            0x5C0,
            0x5C4,
            0x5C8,
            0x5CC,
            0x5D0,
            0x5D4,
            0x5D8,
            0x5DC,
            0x5E0,
            0x5E4,
            0x5E8,
            0x5EC,
            0x5F0,
            0x5F4,
            0x5F8,
            0x5FC,
            0x600,
            0x604,
            0x608,
            0x60C,
            0x610,
            0x614,
            0x618,
            0x61C,
            0x620,
            0x624,
            0x628,
            0x62C,
            0x630,
            0x634,
            0x638,
            0x63C,
            0x640,
            0x644,
            0x648,
            0x64C,
            0x650,
            0x654,
            0x658,
            0x65C,
            0x660,
            0x664,
            0x668,
            0x66C,
            0x670,
            0x674,
            0x678,
            0x67C,
            0x680,
            0x684,
            0x688,
            0x68C,
            0x690,
            0x694,
            0x698,
            0x69C,
            0x6A0,
            0x6A4,
            0x6A8,
            0x6AC,
            0x6B0,
            0x6B4,
            0x6B8,
            0x6BC,
            0x6C0,
            0x6C4,
            0x6C8,
            0x6CC,
            0x6D0,
            0x6D4,
            0x6D8,
            0x6DC,
            0x6E0,
            0x6E4,
            0x6E8,
            0x6EC,
            0x6F0,
            0x6F4,
            0x6F8,
            0x6FC,
            0x700,
            0x704,
            0x708,
            0x70C,
            0x710,
            0x714,
            0x718,
            0x71C,
            0x720,
            0x724,
            0x728,
            0x72C,
            0x730,
            0x734,
            0x738,
            0x73C,
            0x740,
            0x744,
            0x748,
            0x74C,
            0x750,
            0x754,
            0x758,
            0x75C,
            0x760,
            0x764,
            0x768,
            0x76C,
            0x770,
            0x774,
            0x778,
            0x77C,
            0x780,
            0x784,
            0x788,
            0x78C,
            0x790,
            0x794,
            0x798,
            0x79C,
            0x7A0,
            0x7A4,
            0x7A8,
            0x7AC,
            0x7B0,
            0x7B4,
            0x7B8,
            0x7BC,
            0x7C0,
            0x7C4,
            0x7C8,
            0x7CC,
            0x7D0,
            0x7D4,
            0x7D8,
            0x7DC,
            0x7E0,
            0x7E4,
            0x7E8,
            0x7EC,
            0x7F0,
            0x7F4,
            0x7F8,
            0x7FC,
            0x800,
            0x804,
            0x808,
            0x80C,
            0x810,
            0x814,
            0x818,
            0x81C,
            0x820,
            0x824,
            0x828,
            0x82C,
            0x830,
            0x834,
            0x838,
            0x83C,
            0x840,
            0x844,
            0x848,
            0x84C,
            0x850,
            0x854,
            0x858,
            0x85C,
            0x860,
            0x864,
            0x868,
            0x86C,
            0x870,
            0x874,
            0x878,
            0x87C,
            0x880,
            0x884,
            0x888,
            0x88C,
            0x890,
            0x894,
            0x898,
            0x89C,
            0x8A0,
            0x8A4,
            0x8A8,
            0x8AC,
            0x8B0,
            0x8B4,
            0x8B8,
            0x8BC,
            0x8C0,
            0x8C4 };

        private static Smu RyzenAccess;
        private static uint Address;
        private static uint[] Args;

        readonly System.Windows.Forms.Timer UpdateTimer = new System.Windows.Forms.Timer();

        public BindingList<PowerMonitoringItem> Sensors = new BindingList<PowerMonitoringItem>();
        private void FillInData(uint[] table)
        {

            
        }

        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysLong(UIntPtr memAddress, out uint Data);

        public SystemMonitor()
        {
            RyzenAccess = new Smu(false);
            RyzenAccess.Initialize();
            Args = new uint[6];
            RyzenAccess.SendPsmu(0x66, ref Args);
            Address = Args[0];
            RyzenAccess.Deinitialize();
            float TestValue = ReadFloat(Address, (uint)0x300);
            if (TestValue == 0.0)
            {
                PMTableVersion = 0x00370005;
            }
            else
            {
                PMTableVersion = 0x00370004;
            }



            UpdateTimer.Tick += new EventHandler(UptateTimer_Tick);
            UpdateTimer.Interval = 2000;
            UpdateTimer.Enabled = true;

            InitializeComponent();
            CpuData.DataSource = Sensors;

            FillInTable();
            
        }


        private static float ReadFloat(uint Address, uint Offset)
        {
            uint Data = 0;
            GetPhysLong((UIntPtr)(Address + Offset), out Data);

            byte[] bytes = new byte[4];
            bytes = BitConverter.GetBytes(Data);

            float PmData = BitConverter.ToSingle(bytes, 0);
            //Console.WriteLine($"0x{Address + Offset * 4,8:X8} | {PmData:F}");
            return PmData;
        }


        private void SystemMonitor_Shown(object sender, EventArgs e)
        {
            UpdateTimer.Start();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = Convert.ToInt32(numericUpDownInterval.Value);
        }
        public PowerMonitoringItem CreatePowerMonitoringItem(string Name, uint Offset)
        {
            return new PowerMonitoringItem { 
                Offset = $"{Offset:D}", 
                Sensor = Name, 
                Value = $"{ReadFloat(Address, Offset):F}" };

        }
        public void UptateTimer_Tick(object sender, EventArgs e)
        {
            RefreshData();
            PowerSetting CurrentSetting = JsonConvert.DeserializeObject<PowerSetting>(File.ReadLines("CurrentSettings.json").First());
            if(CurrentSetting.SmartReapply)
            {
                CurrentSetting.CheckAndReapply(Address);
            }
        }

        private void FillInTable()
        {

            Sensors.Clear();
            switch (PMTableVersion)
            {
                case (uint)0x00370004:
                    Sensors.Add(CreatePowerMonitoringItem("STAPM LIMIT", (uint)0x0));
                    Sensors.Add(CreatePowerMonitoringItem("STAPM VALUE", (uint)0x4));
                    Sensors.Add(CreatePowerMonitoringItem("PPT LIMIT FAST", (uint)0x8));
                    Sensors.Add(CreatePowerMonitoringItem("PPT VALUE FAST", (uint)0xC));
                    Sensors.Add(CreatePowerMonitoringItem("PPT LIMIT SLOW", (uint)0x10));
                    Sensors.Add(CreatePowerMonitoringItem("PPT VALUE SLOW", (uint)0x14));
                    Sensors.Add(CreatePowerMonitoringItem("PPT LIMIT APU", (uint)0x18));
                    Sensors.Add(CreatePowerMonitoringItem("PPT VALUE APU", (uint)0x1C));
                    Sensors.Add(CreatePowerMonitoringItem("TDC LIMIT VDD", (uint)0x20));
                    Sensors.Add(CreatePowerMonitoringItem("TDC VALUE VDD", (uint)0x24));
                    Sensors.Add(CreatePowerMonitoringItem("TDC LIMIT SOC", (uint)0x28));
                    Sensors.Add(CreatePowerMonitoringItem("TDC VALUE SOC", (uint)0x2C));
                    Sensors.Add(CreatePowerMonitoringItem("EDC LIMIT VDD", (uint)0x30));
                    Sensors.Add(CreatePowerMonitoringItem("EDC VALUE VDD", (uint)0x34));
                    Sensors.Add(CreatePowerMonitoringItem("EDC LIMIT SOC", (uint)0x38));
                    Sensors.Add(CreatePowerMonitoringItem("EDC VALUE SOC", (uint)0x3C));
                    Sensors.Add(CreatePowerMonitoringItem("THM LIMIT CORE", (uint)0x40));
                    Sensors.Add(CreatePowerMonitoringItem("THM VALUE CORE", (uint)0x44));
                    Sensors.Add(CreatePowerMonitoringItem("THM LIMIT GFX", (uint)0x48));
                    Sensors.Add(CreatePowerMonitoringItem("THM VALUE GFX", (uint)0x4C));
                    Sensors.Add(CreatePowerMonitoringItem("THM LIMIT SOC", (uint)0x50));
                    Sensors.Add(CreatePowerMonitoringItem("THM VALUE SOC", (uint)0x54));
                    Sensors.Add(CreatePowerMonitoringItem("STT LIMIT APU", (uint)0x58));
                    Sensors.Add(CreatePowerMonitoringItem("STT VALUE APU", (uint)0x5C));
                    Sensors.Add(CreatePowerMonitoringItem("STT LIMIT dGPU", (uint)0x60));
                    Sensors.Add(CreatePowerMonitoringItem("STT VALUE dGPU", (uint)0x64));
                    Sensors.Add(CreatePowerMonitoringItem("FIT LIMIT", (uint)0x68));
                    Sensors.Add(CreatePowerMonitoringItem("FIT VALUE", (uint)0x6C));
                    Sensors.Add(CreatePowerMonitoringItem("VID LIMIT", (uint)0x70));
                    Sensors.Add(CreatePowerMonitoringItem("VID VALUE", (uint)0x74));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 LIMIT VDD", (uint)0x78));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 RESIDENCY VDD", (uint)0x7C));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 LIMIT SOC", (uint)0x80));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 RESIDENCY SOC", (uint)0x84));
                    Sensors.Add(CreatePowerMonitoringItem("VDDCR CPU POWER", (uint)0x88));
                    Sensors.Add(CreatePowerMonitoringItem("VDDCR SOC POWER", (uint)0x8C));
                    Sensors.Add(CreatePowerMonitoringItem("VDDIO MEM POWER", (uint)0x90));
                    Sensors.Add(CreatePowerMonitoringItem("ROC POWER", (uint)0x94));
                    Sensors.Add(CreatePowerMonitoringItem("SOCKET POWER", (uint)0x98));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK GLOBAL FREQ", (uint)0x9C));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK STAPM FREQ", (uint)0xA0));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PPT FAST FREQ", (uint)0xA4));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PPT SLOW FREQ", (uint)0xA8));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PPT APU ONLY FREQ", (uint)0xAC));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK TDC FREQ", (uint)0xB0));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK THM FREQ", (uint)0xB4));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK HTFMAX FREQ", (uint)0xB8));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PROCHOT FREQ", (uint)0xBC));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK VOLTAGE FREQ", (uint)0xC0));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK CCA FREQ", (uint)0xC4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK GLOBAL FREQ", (uint)0xC8));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK STAPM FREQ", (uint)0xCC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PPT FAST FREQ", (uint)0xD0));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PPT SLOW FREQ", (uint)0xD4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PPT APU ONLY FREQ", (uint)0xD8));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK TDC FREQ", (uint)0xDC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK THM FREQ", (uint)0xE0));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK HTFMAX FREQ", (uint)0xE4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PROCHOT FREQ", (uint)0xE8));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK VOLTAGE FREQ", (uint)0xEC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK CCA FREQ", (uint)0xF0));
                    Sensors.Add(CreatePowerMonitoringItem("FIT VOLTAGE", (uint)0xF4));
                    Sensors.Add(CreatePowerMonitoringItem("LATCHUP VOLTAGE", (uint)0xF8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SETPOINT", (uint)0xFC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE BUSY", (uint)0x100));
                    Sensors.Add(CreatePowerMonitoringItem("GFX SETPOINT", (uint)0x104));
                    Sensors.Add(CreatePowerMonitoringItem("GFX DPM BUSY", (uint)0x108));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK CCX SETPOINT", (uint)0x10C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK CCX BUSY", (uint)0x110));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK GFX SETPOINT", (uint)0x114));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK GFX BUSY", (uint)0x118));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK IO SETPOINT", (uint)0x11C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK IO BUSY", (uint)0x120));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK DRAM SETPOINT", (uint)0x124));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK DRAM BUSY", (uint)0x128));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK SETPOINT", (uint)0x12C));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK BUSY", (uint)0x130));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 0", (uint)0x134));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 1", (uint)0x138));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 2", (uint)0x13C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 3", (uint)0x140));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 0", (uint)0x144));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 1", (uint)0x148));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 2", (uint)0x14C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 3", (uint)0x150));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 0", (uint)0x154));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 1", (uint)0x158));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 2", (uint)0x15C));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 3", (uint)0x160));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 0", (uint)0x164));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 1", (uint)0x168));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 2", (uint)0x16C));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 3", (uint)0x170));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 0", (uint)0x174));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 1", (uint)0x178));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 2", (uint)0x17C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 3", (uint)0x180));
                    Sensors.Add(CreatePowerMonitoringItem("CPU SET VOLTAGE", (uint)0x184));
                    Sensors.Add(CreatePowerMonitoringItem("CPU TELEMETRY VOLTAGE", (uint)0x188));
                    Sensors.Add(CreatePowerMonitoringItem("CPU TELEMETRY CURRENT", (uint)0x18C));
                    Sensors.Add(CreatePowerMonitoringItem("CPU TELEMETRY POWER", (uint)0x190));
                    Sensors.Add(CreatePowerMonitoringItem("SOC SET VOLTAGE", (uint)0x194));
                    Sensors.Add(CreatePowerMonitoringItem("SOC TELEMETRY VOLTAGE", (uint)0x198));
                    Sensors.Add(CreatePowerMonitoringItem("SOC TELEMETRY CURRENT", (uint)0x19C));
                    Sensors.Add(CreatePowerMonitoringItem("SOC TELEMETRY POWER", (uint)0x1A0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail0 voltage", (uint)0x1A4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail1 voltage", (uint)0x1A8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail4 voltage", (uint)0x1AC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail5 voltage", (uint)0x1B0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail6 voltage", (uint)0x1B4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail7 voltage", (uint)0x1B8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail8 voltage", (uint)0x1BC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail9 voltage", (uint)0x1C0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail10 voltage", (uint)0x1C4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail11 voltage", (uint)0x1C8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail12 voltage", (uint)0x1CC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail13 voltage", (uint)0x1D0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail14 voltage", (uint)0x1D4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail15 voltage", (uint)0x1D8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail16 voltage", (uint)0x1DC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail17 voltage", (uint)0x1E0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail18 voltage", (uint)0x1E4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail19 voltage", (uint)0x1E8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail20 voltage", (uint)0x1EC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail21 voltage", (uint)0x1F0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail0 current", (uint)0x1F4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail1 current", (uint)0x1F8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail4 current", (uint)0x1FC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail5 current", (uint)0x200));
                    Sensors.Add(CreatePowerMonitoringItem("Rail6 current", (uint)0x204));
                    Sensors.Add(CreatePowerMonitoringItem("Rail7 current", (uint)0x208));
                    Sensors.Add(CreatePowerMonitoringItem("Rail8 current", (uint)0x20C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail9 current", (uint)0x210));
                    Sensors.Add(CreatePowerMonitoringItem("Rail10 current", (uint)0x214));
                    Sensors.Add(CreatePowerMonitoringItem("Rail11 current", (uint)0x218));
                    Sensors.Add(CreatePowerMonitoringItem("Rail12 current", (uint)0x21C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail13 current", (uint)0x220));
                    Sensors.Add(CreatePowerMonitoringItem("Rail14 current", (uint)0x224));
                    Sensors.Add(CreatePowerMonitoringItem("Rail15 current", (uint)0x228));
                    Sensors.Add(CreatePowerMonitoringItem("Rail16 current", (uint)0x22C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail17 current", (uint)0x230));
                    Sensors.Add(CreatePowerMonitoringItem("Rail18 current", (uint)0x234));
                    Sensors.Add(CreatePowerMonitoringItem("Rail19 current", (uint)0x238));
                    Sensors.Add(CreatePowerMonitoringItem("Rail20 current", (uint)0x23C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail21 current", (uint)0x240));
                    Sensors.Add(CreatePowerMonitoringItem("Rail0 power", (uint)0x244));
                    Sensors.Add(CreatePowerMonitoringItem("Rail1 power", (uint)0x248));
                    Sensors.Add(CreatePowerMonitoringItem("Rail4 power", (uint)0x24C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail5 power", (uint)0x250));
                    Sensors.Add(CreatePowerMonitoringItem("Rail6 power", (uint)0x254));
                    Sensors.Add(CreatePowerMonitoringItem("Rail7 power", (uint)0x258));
                    Sensors.Add(CreatePowerMonitoringItem("Rail8 power", (uint)0x25C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail9 power", (uint)0x260));
                    Sensors.Add(CreatePowerMonitoringItem("Rail10 power", (uint)0x264));
                    Sensors.Add(CreatePowerMonitoringItem("Rail11 power", (uint)0x268));
                    Sensors.Add(CreatePowerMonitoringItem("Rail12 power", (uint)0x26C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail13 power", (uint)0x270));
                    Sensors.Add(CreatePowerMonitoringItem("Rail14 power", (uint)0x274));
                    Sensors.Add(CreatePowerMonitoringItem("Rail15 power", (uint)0x278));
                    Sensors.Add(CreatePowerMonitoringItem("Rail16 power", (uint)0x27C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail17 power", (uint)0x280));
                    Sensors.Add(CreatePowerMonitoringItem("Rail18 power", (uint)0x284));
                    Sensors.Add(CreatePowerMonitoringItem("Rail19 power", (uint)0x288));
                    Sensors.Add(CreatePowerMonitoringItem("Rail20 power", (uint)0x28C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail21 power", (uint)0x290));
                    Sensors.Add(CreatePowerMonitoringItem("DfBusy", (uint)0x294));
                    Sensors.Add(CreatePowerMonitoringItem("VcnBusy", (uint)0x298));
                    Sensors.Add(CreatePowerMonitoringItem("IohcBusy", (uint)0x29C));
                    Sensors.Add(CreatePowerMonitoringItem("MmhubBusy", (uint)0x2A0));
                    Sensors.Add(CreatePowerMonitoringItem("AthubBusy", (uint)0x2A4));
                    Sensors.Add(CreatePowerMonitoringItem("OsssysBusy", (uint)0x2A8));
                    Sensors.Add(CreatePowerMonitoringItem("HdpBusy", (uint)0x2AC));
                    Sensors.Add(CreatePowerMonitoringItem("SdmaBusy", (uint)0x2B0));
                    Sensors.Add(CreatePowerMonitoringItem("ShubBusy", (uint)0x2B4));
                    Sensors.Add(CreatePowerMonitoringItem("BifBusy", (uint)0x2B8));
                    Sensors.Add(CreatePowerMonitoringItem("AcpBusy", (uint)0x2BC));
                    Sensors.Add(CreatePowerMonitoringItem("Sst0Busy", (uint)0x2C0));
                    Sensors.Add(CreatePowerMonitoringItem("Sst1Busy", (uint)0x2C4));
                    Sensors.Add(CreatePowerMonitoringItem("Usb0Busy", (uint)0x2C8));
                    Sensors.Add(CreatePowerMonitoringItem("Usb1Busy", (uint)0x2CC));
                    Sensors.Add(CreatePowerMonitoringItem("CCM Reads", (uint)0x2D0));
                    Sensors.Add(CreatePowerMonitoringItem("CCM Writes", (uint)0x2D4));
                    Sensors.Add(CreatePowerMonitoringItem("GCM 64B Reads", (uint)0x2D8));
                    Sensors.Add(CreatePowerMonitoringItem("GCM 64B Writes", (uint)0x2DC));
                    Sensors.Add(CreatePowerMonitoringItem("GCM 32B ReadsWrites", (uint)0x2E0));
                    Sensors.Add(CreatePowerMonitoringItem("MMHUB Reads", (uint)0x2E4));
                    Sensors.Add(CreatePowerMonitoringItem("MMHUB Writes", (uint)0x2E8));
                    Sensors.Add(CreatePowerMonitoringItem("DCE Reads", (uint)0x2EC));
                    Sensors.Add(CreatePowerMonitoringItem("IO ReadsWrites", (uint)0x2F0));
                    Sensors.Add(CreatePowerMonitoringItem("CS Reads", (uint)0x2F4));
                    Sensors.Add(CreatePowerMonitoringItem("CS Writes", (uint)0x2F8));
                    Sensors.Add(CreatePowerMonitoringItem("MaxDramBW", (uint)0x2FC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 0", (uint)0x300));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 1", (uint)0x304));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 2", (uint)0x308));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 3", (uint)0x30C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 4", (uint)0x310));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 5", (uint)0x314));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 6", (uint)0x318));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 7", (uint)0x31C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 0", (uint)0x320));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 1", (uint)0x324));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 2", (uint)0x328));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 3", (uint)0x32C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 4", (uint)0x330));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 5", (uint)0x334));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 6", (uint)0x338));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 7", (uint)0x33C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 0", (uint)0x340));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 1", (uint)0x344));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 2", (uint)0x348));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 3", (uint)0x34C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 4", (uint)0x350));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 5", (uint)0x354));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 6", (uint)0x358));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 7", (uint)0x35C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 0", (uint)0x360));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 1", (uint)0x364));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 2", (uint)0x368));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 3", (uint)0x36C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 4", (uint)0x370));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 5", (uint)0x374));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 6", (uint)0x378));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 7", (uint)0x37C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 0", (uint)0x380));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 1", (uint)0x384));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 2", (uint)0x388));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 3", (uint)0x38C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 4", (uint)0x390));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 5", (uint)0x394));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 6", (uint)0x398));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 7", (uint)0x39C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 0", (uint)0x3A0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 1", (uint)0x3A4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 2", (uint)0x3A8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 3", (uint)0x3AC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 4", (uint)0x3B0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 5", (uint)0x3B4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 6", (uint)0x3B8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 7", (uint)0x3BC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 0", (uint)0x3C0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 1", (uint)0x3C4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 2", (uint)0x3C8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 3", (uint)0x3CC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 4", (uint)0x3D0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 5", (uint)0x3D4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 6", (uint)0x3D8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 7", (uint)0x3DC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 0", (uint)0x3E0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 1", (uint)0x3E4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 2", (uint)0x3E8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 3", (uint)0x3EC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 4", (uint)0x3F0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 5", (uint)0x3F4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 6", (uint)0x3F8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 7", (uint)0x3FC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 0", (uint)0x400));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 1", (uint)0x404));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 2", (uint)0x408));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 3", (uint)0x40C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 4", (uint)0x410));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 5", (uint)0x414));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 6", (uint)0x418));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 7", (uint)0x41C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 0", (uint)0x420));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 1", (uint)0x424));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 2", (uint)0x428));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 3", (uint)0x42C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 4", (uint)0x430));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 5", (uint)0x434));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 6", (uint)0x438));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 7", (uint)0x43C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 0", (uint)0x440));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 1", (uint)0x444));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 2", (uint)0x448));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 3", (uint)0x44C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 4", (uint)0x450));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 5", (uint)0x454));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 6", (uint)0x458));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 7", (uint)0x45C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 0", (uint)0x460));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 1", (uint)0x464));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 2", (uint)0x468));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 3", (uint)0x46C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 4", (uint)0x470));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 5", (uint)0x474));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 6", (uint)0x478));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 7", (uint)0x47C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 0", (uint)0x480));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 1", (uint)0x484));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 2", (uint)0x488));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 3", (uint)0x48C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 4", (uint)0x490));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 5", (uint)0x494));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 6", (uint)0x498));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 7", (uint)0x49C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 0", (uint)0x4A0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 1", (uint)0x4A4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 2", (uint)0x4A8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 3", (uint)0x4AC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 4", (uint)0x4B0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 5", (uint)0x4B4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 6", (uint)0x4B8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 7", (uint)0x4BC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 0", (uint)0x4C0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 1", (uint)0x4C4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 2", (uint)0x4C8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 3", (uint)0x4CC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 4", (uint)0x4D0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 5", (uint)0x4D4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 6", (uint)0x4D8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 7", (uint)0x4DC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 0", (uint)0x4E0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 1", (uint)0x4E4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 2", (uint)0x4E8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 3", (uint)0x4EC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 4", (uint)0x4F0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 5", (uint)0x4F4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 6", (uint)0x4F8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 7", (uint)0x4FC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 0", (uint)0x500));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 1", (uint)0x504));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 2", (uint)0x508));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 3", (uint)0x50C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 4", (uint)0x510));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 5", (uint)0x514));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 6", (uint)0x518));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 7", (uint)0x51C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 0", (uint)0x520));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 1", (uint)0x524));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 2", (uint)0x528));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 3", (uint)0x52C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 4", (uint)0x530));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 5", (uint)0x534));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 6", (uint)0x538));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 7", (uint)0x53C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 LOGIC POWER 0", (uint)0x540));
                    Sensors.Add(CreatePowerMonitoringItem("L3 LOGIC POWER 1", (uint)0x544));
                    Sensors.Add(CreatePowerMonitoringItem("L3 VDDM POWER 0", (uint)0x548));
                    Sensors.Add(CreatePowerMonitoringItem("L3 VDDM POWER 1", (uint)0x54C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 TEMP 0", (uint)0x550));
                    Sensors.Add(CreatePowerMonitoringItem("L3 TEMP 1", (uint)0x554));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FIT 0", (uint)0x558));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FIT 1", (uint)0x55C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 IDDMAX 0", (uint)0x560));
                    Sensors.Add(CreatePowerMonitoringItem("L3 IDDMAX 1", (uint)0x564));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FREQ 0", (uint)0x568));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FREQ 1", (uint)0x56C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CKS FDD 0", (uint)0x570));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CKS FDD 1", (uint)0x574));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA THRESHOLD 0", (uint)0x578));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA THRESHOLD 1", (uint)0x57C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA CAC 0", (uint)0x580));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA CAC 1", (uint)0x584));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA ACTIVATION 0", (uint)0x588));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA ACTIVATION 1", (uint)0x58C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC LIMIT 0", (uint)0x590));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC LIMIT 1", (uint)0x594));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC CAC 0", (uint)0x598));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC CAC 1", (uint)0x59C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC RESIDENCY 0", (uint)0x5A0));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC RESIDENCY 1", (uint)0x5A4));
                    Sensors.Add(CreatePowerMonitoringItem("GFX VOLTAGE", (uint)0x5A8));
                    Sensors.Add(CreatePowerMonitoringItem("GFX TEMP", (uint)0x5AC));
                    Sensors.Add(CreatePowerMonitoringItem("GFX IDDMAX", (uint)0x5B0));
                    Sensors.Add(CreatePowerMonitoringItem("GFX FREQ", (uint)0x5B4));
                    Sensors.Add(CreatePowerMonitoringItem("GFX FREQEFF", (uint)0x5B8));
                    Sensors.Add(CreatePowerMonitoringItem("GFX BUSY", (uint)0x5BC));
                    Sensors.Add(CreatePowerMonitoringItem("GFX CGPG", (uint)0x5C0));
                    Sensors.Add(CreatePowerMonitoringItem("GFX EDC LIMIT", (uint)0x5C4));
                    Sensors.Add(CreatePowerMonitoringItem("GFX EDC RESIDENCY", (uint)0x5C8));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ", (uint)0x5CC));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ", (uint)0x5D0));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ", (uint)0x5D4));
                    Sensors.Add(CreatePowerMonitoringItem("VCLK FREQ", (uint)0x5D8));
                    Sensors.Add(CreatePowerMonitoringItem("DCLK FREQ", (uint)0x5DC));
                    Sensors.Add(CreatePowerMonitoringItem("SOCCLK FREQ", (uint)0x5E0));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK FREQ", (uint)0x5E4));
                    Sensors.Add(CreatePowerMonitoringItem("SHUBCLK FREQ", (uint)0x5E8));
                    Sensors.Add(CreatePowerMonitoringItem("MP0CLK FREQ", (uint)0x5EC));
                    Sensors.Add(CreatePowerMonitoringItem("DCFCLK FREQ", (uint)0x5F0));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQEFF", (uint)0x5F4));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQEFF", (uint)0x5F8));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQEFF", (uint)0x5FC));
                    Sensors.Add(CreatePowerMonitoringItem("VCLK FREQEFF", (uint)0x600));
                    Sensors.Add(CreatePowerMonitoringItem("DCLK FREQEFF", (uint)0x604));
                    Sensors.Add(CreatePowerMonitoringItem("SOCCLK FREQEFF", (uint)0x608));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK FREQEFF", (uint)0x60C));
                    Sensors.Add(CreatePowerMonitoringItem("SHUBCLK FREQEFF", (uint)0x610));
                    Sensors.Add(CreatePowerMonitoringItem("MP0CLK FREQEFF", (uint)0x614));
                    Sensors.Add(CreatePowerMonitoringItem("DCFCLK FREQEFF", (uint)0x618));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 0", (uint)0x61C));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 1", (uint)0x620));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 2", (uint)0x624));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 3", (uint)0x628));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 4", (uint)0x62C));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 5", (uint)0x630));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 6", (uint)0x634));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 7", (uint)0x638));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 0", (uint)0x63C));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 1", (uint)0x640));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 2", (uint)0x644));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 3", (uint)0x648));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 4", (uint)0x64C));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 5", (uint)0x650));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 6", (uint)0x654));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 7", (uint)0x658));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 0", (uint)0x65C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 1", (uint)0x660));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 2", (uint)0x664));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 3", (uint)0x668));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 4", (uint)0x66C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 5", (uint)0x670));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 6", (uint)0x674));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 7", (uint)0x678));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 0", (uint)0x67C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 1", (uint)0x680));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 2", (uint)0x684));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 3", (uint)0x688));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 4", (uint)0x68C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 5", (uint)0x690));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 6", (uint)0x694));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 7", (uint)0x698));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 0", (uint)0x69C));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 1", (uint)0x6A0));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 2", (uint)0x6A4));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 3", (uint)0x6A8));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 4", (uint)0x6AC));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 5", (uint)0x6B0));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 6", (uint)0x6B4));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 7", (uint)0x6B8));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 0", (uint)0x6BC));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 1", (uint)0x6C0));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 2", (uint)0x6C4));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 3", (uint)0x6C8));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 4", (uint)0x6CC));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 5", (uint)0x6D0));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 6", (uint)0x6D4));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 7", (uint)0x6D8));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 0", (uint)0x6DC));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 1", (uint)0x6E0));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 2", (uint)0x6E4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 3", (uint)0x6E8));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 4", (uint)0x6EC));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 5", (uint)0x6F0));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 6", (uint)0x6F4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 7", (uint)0x6F8));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 0", (uint)0x6FC));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 1", (uint)0x700));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 2", (uint)0x704));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 3", (uint)0x708));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 4", (uint)0x70C));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 5", (uint)0x710));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 6", (uint)0x714));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 7", (uint)0x718));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 0", (uint)0x71C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 1", (uint)0x720));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 2", (uint)0x724));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 3", (uint)0x728));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 4", (uint)0x72C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 5", (uint)0x730));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 6", (uint)0x734));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 7", (uint)0x738));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 0", (uint)0x73C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 1", (uint)0x740));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 2", (uint)0x744));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 3", (uint)0x748));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 4", (uint)0x74C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 5", (uint)0x750));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 6", (uint)0x754));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 7", (uint)0x758));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 0", (uint)0x75C));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 1", (uint)0x760));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 2", (uint)0x764));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 3", (uint)0x768));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 4", (uint)0x76C));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 5", (uint)0x770));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 6", (uint)0x774));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 7", (uint)0x778));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 0", (uint)0x77C));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 1", (uint)0x780));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 2", (uint)0x784));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 3", (uint)0x788));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 4", (uint)0x78C));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 5", (uint)0x790));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 6", (uint)0x794));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 7", (uint)0x798));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 0", (uint)0x79C));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 1", (uint)0x7A0));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 2", (uint)0x7A4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 3", (uint)0x7A8));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 4", (uint)0x7AC));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 5", (uint)0x7B0));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 6", (uint)0x7B4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 7", (uint)0x7B8));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 0", (uint)0x7BC));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 1", (uint)0x7C0));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 2", (uint)0x7C4));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 3", (uint)0x7C8));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 4", (uint)0x7CC));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 5", (uint)0x7D0));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 6", (uint)0x7D4));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 7", (uint)0x7D8));
                    Sensors.Add(CreatePowerMonitoringItem("CPUOFF", (uint)0x7DC));
                    Sensors.Add(CreatePowerMonitoringItem("CPUOFF count", (uint)0x7E0));
                    Sensors.Add(CreatePowerMonitoringItem("GFXOFF", (uint)0x7E4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXOFF count", (uint)0x7E8));
                    Sensors.Add(CreatePowerMonitoringItem("VDDOFF", (uint)0x7EC));
                    Sensors.Add(CreatePowerMonitoringItem("VDDOFF count", (uint)0x7F0));
                    Sensors.Add(CreatePowerMonitoringItem("ULV", (uint)0x7F4));
                    Sensors.Add(CreatePowerMonitoringItem("ULV count", (uint)0x7F8));
                    Sensors.Add(CreatePowerMonitoringItem("S0i2", (uint)0x7FC));
                    Sensors.Add(CreatePowerMonitoringItem("S0i2 count", (uint)0x800));
                    Sensors.Add(CreatePowerMonitoringItem("WhisperMode", (uint)0x804));
                    Sensors.Add(CreatePowerMonitoringItem("WhisperMode count", (uint)0x808));
                    Sensors.Add(CreatePowerMonitoringItem("SelfRefresh0", (uint)0x80C));
                    Sensors.Add(CreatePowerMonitoringItem("SelfRefresh1", (uint)0x810));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 0", (uint)0x814));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 1", (uint)0x818));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 2", (uint)0x81C));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 3", (uint)0x820));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 4", (uint)0x824));
                    Sensors.Add(CreatePowerMonitoringItem("UINT8 T POWER SOURCE (4)", (uint)0x828));
                    Sensors.Add(CreatePowerMonitoringItem("dGPU POWER", (uint)0x82C));
                    Sensors.Add(CreatePowerMonitoringItem("dGPU GFX BUSY", (uint)0x830));
                    Sensors.Add(CreatePowerMonitoringItem("dGPU FREQ TARGET", (uint)0x834));
                    Sensors.Add(CreatePowerMonitoringItem("V VDDM", (uint)0x838));
                    Sensors.Add(CreatePowerMonitoringItem("V VDDP", (uint)0x83C));
                    Sensors.Add(CreatePowerMonitoringItem("DDR PHY POWER", (uint)0x840));
                    Sensors.Add(CreatePowerMonitoringItem("IO VDDIO MEM POWER", (uint)0x844));
                    Sensors.Add(CreatePowerMonitoringItem("IO VDD18 POWER", (uint)0x848));
                    Sensors.Add(CreatePowerMonitoringItem("IO DISPLAY POWER", (uint)0x84C));
                    Sensors.Add(CreatePowerMonitoringItem("IO USB POWER", (uint)0x850));
                    Sensors.Add(CreatePowerMonitoringItem("ULV VOLTAGE", (uint)0x854));
                    Sensors.Add(CreatePowerMonitoringItem("PEAK TEMP", (uint)0x858));
                    Sensors.Add(CreatePowerMonitoringItem("PEAK VOLTAGE", (uint)0x85C));
                    Sensors.Add(CreatePowerMonitoringItem("AVG CORE COUNT", (uint)0x860));
                    Sensors.Add(CreatePowerMonitoringItem("MAX VOLTAGE", (uint)0x864));
                    Sensors.Add(CreatePowerMonitoringItem("DC BTC", (uint)0x868));
                    Sensors.Add(CreatePowerMonitoringItem("CSTATE BOOST", (uint)0x86C));
                    Sensors.Add(CreatePowerMonitoringItem("PROCHOT", (uint)0x870));
                    Sensors.Add(CreatePowerMonitoringItem("PWM", (uint)0x874));
                    Sensors.Add(CreatePowerMonitoringItem("FPS", (uint)0x878));
                    Sensors.Add(CreatePowerMonitoringItem("DISPLAY COUNT", (uint)0x87C));
                    Sensors.Add(CreatePowerMonitoringItem("StapmTimeConstant", (uint)0x880));
                    Sensors.Add(CreatePowerMonitoringItem("SlowPPTTimeConstant", (uint)0x884));
                    Sensors.Add(CreatePowerMonitoringItem("MP1CLK", (uint)0x888));
                    Sensors.Add(CreatePowerMonitoringItem("MP2CLK", (uint)0x88C));
                    Sensors.Add(CreatePowerMonitoringItem("SMNCLK", (uint)0x890));
                    Sensors.Add(CreatePowerMonitoringItem("ACLK", (uint)0x894));
                    Sensors.Add(CreatePowerMonitoringItem("DISPCLK", (uint)0x898));
                    Sensors.Add(CreatePowerMonitoringItem("DPREFCLK", (uint)0x89C));
                    Sensors.Add(CreatePowerMonitoringItem("DPPCLK", (uint)0x8A0));
                    Sensors.Add(CreatePowerMonitoringItem("SMU BUSY", (uint)0x8A4));
                    Sensors.Add(CreatePowerMonitoringItem("SMU SKIP COUNTER", (uint)0x8A8));
                    break;
                case (UInt32)0x00370005:
                    Sensors.Add(CreatePowerMonitoringItem("STAPM LIMIT", (uint)0x0));
                    Sensors.Add(CreatePowerMonitoringItem("STAPM VALUE", (uint)0x4));
                    Sensors.Add(CreatePowerMonitoringItem("PPT LIMIT FAST", (uint)0x8));
                    Sensors.Add(CreatePowerMonitoringItem("PPT VALUE FAST", (uint)0xC));
                    Sensors.Add(CreatePowerMonitoringItem("PPT LIMIT SLOW", (uint)0x10));
                    Sensors.Add(CreatePowerMonitoringItem("PPT VALUE SLOW", (uint)0x14));
                    Sensors.Add(CreatePowerMonitoringItem("PPT LIMIT APU", (uint)0x18));
                    Sensors.Add(CreatePowerMonitoringItem("PPT VALUE APU", (uint)0x1C));
                    Sensors.Add(CreatePowerMonitoringItem("TDC LIMIT VDD", (uint)0x20));
                    Sensors.Add(CreatePowerMonitoringItem("TDC VALUE VDD", (uint)0x24));
                    Sensors.Add(CreatePowerMonitoringItem("TDC LIMIT SOC", (uint)0x28));
                    Sensors.Add(CreatePowerMonitoringItem("TDC VALUE SOC", (uint)0x2C));
                    Sensors.Add(CreatePowerMonitoringItem("EDC LIMIT VDD", (uint)0x30));
                    Sensors.Add(CreatePowerMonitoringItem("EDC VALUE VDD", (uint)0x34));
                    Sensors.Add(CreatePowerMonitoringItem("EDC LIMIT SOC", (uint)0x38));
                    Sensors.Add(CreatePowerMonitoringItem("EDC VALUE SOC", (uint)0x3C));
                    Sensors.Add(CreatePowerMonitoringItem("THM LIMIT CORE", (uint)0x40));
                    Sensors.Add(CreatePowerMonitoringItem("THM VALUE CORE", (uint)0x44));
                    Sensors.Add(CreatePowerMonitoringItem("THM LIMIT GFX", (uint)0x48));
                    Sensors.Add(CreatePowerMonitoringItem("THM VALUE GFX", (uint)0x4C));
                    Sensors.Add(CreatePowerMonitoringItem("THM LIMIT SOC", (uint)0x50));
                    Sensors.Add(CreatePowerMonitoringItem("THM VALUE SOC", (uint)0x54));
                    Sensors.Add(CreatePowerMonitoringItem("STT LIMIT APU", (uint)0x58));
                    Sensors.Add(CreatePowerMonitoringItem("STT VALUE APU", (uint)0x5C));
                    Sensors.Add(CreatePowerMonitoringItem("STT LIMIT dGPU", (uint)0x60));
                    Sensors.Add(CreatePowerMonitoringItem("STT VALUE dGPU", (uint)0x64));
                    Sensors.Add(CreatePowerMonitoringItem("FIT LIMIT", (uint)0x68));
                    Sensors.Add(CreatePowerMonitoringItem("FIT VALUE", (uint)0x6C));
                    Sensors.Add(CreatePowerMonitoringItem("VID LIMIT", (uint)0x70));
                    Sensors.Add(CreatePowerMonitoringItem("VID VALUE", (uint)0x74));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 LIMIT VDD", (uint)0x78));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 RESIDENCY VDD", (uint)0x7C));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 LIMIT SOC", (uint)0x80));
                    Sensors.Add(CreatePowerMonitoringItem("PSI0 RESIDENCY SOC", (uint)0x84));
                    Sensors.Add(CreatePowerMonitoringItem("VDDCR CPU POWER", (uint)0x88));
                    Sensors.Add(CreatePowerMonitoringItem("VDDCR SOC POWER", (uint)0x8C));
                    Sensors.Add(CreatePowerMonitoringItem("VDDIO MEM POWER", (uint)0x90));
                    Sensors.Add(CreatePowerMonitoringItem("ROC POWER", (uint)0x94));
                    Sensors.Add(CreatePowerMonitoringItem("SOCKET POWER", (uint)0x98));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK GLOBAL FREQ", (uint)0x9C));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK STAPM FREQ", (uint)0xA0));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PPT FAST FREQ", (uint)0xA4));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PPT SLOW FREQ", (uint)0xA8));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PPT APU ONLY FREQ", (uint)0xAC));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK TDC FREQ", (uint)0xB0));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK THM FREQ", (uint)0xB4));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK HTFMAX FREQ", (uint)0xB8));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK PROCHOT FREQ", (uint)0xBC));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK VOLTAGE FREQ", (uint)0xC0));
                    Sensors.Add(CreatePowerMonitoringItem("CCLK CCA FREQ", (uint)0xC4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK GLOBAL FREQ", (uint)0xC8));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK STAPM FREQ", (uint)0xCC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PPT FAST FREQ", (uint)0xD0));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PPT SLOW FREQ", (uint)0xD4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PPT APU ONLY FREQ", (uint)0xD8));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK TDC FREQ", (uint)0xDC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK THM FREQ", (uint)0xE0));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK HTFMAX FREQ", (uint)0xE4));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK PROCHOT FREQ", (uint)0xE8));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK VOLTAGE FREQ", (uint)0xEC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXCLK CCA FREQ", (uint)0xF0));
                    Sensors.Add(CreatePowerMonitoringItem("FIT VOLTAGE", (uint)0xF4));
                    Sensors.Add(CreatePowerMonitoringItem("LATCHUP VOLTAGE", (uint)0xF8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SETPOINT", (uint)0xFC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE BUSY", (uint)0x100));
                    Sensors.Add(CreatePowerMonitoringItem("GFX SETPOINT", (uint)0x104));
                    Sensors.Add(CreatePowerMonitoringItem("GFX DPM BUSY", (uint)0x108));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK CCX SETPOINT", (uint)0x10C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK CCX BUSY", (uint)0x110));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK GFX SETPOINT", (uint)0x114));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK GFX BUSY", (uint)0x118));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK IO SETPOINT", (uint)0x11C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK IO BUSY", (uint)0x120));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK DRAM SETPOINT", (uint)0x124));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK DRAM BUSY", (uint)0x128));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK SETPOINT", (uint)0x12C));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK BUSY", (uint)0x130));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 0", (uint)0x134));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 1", (uint)0x138));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 2", (uint)0x13C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK RESIDENCY 3", (uint)0x140));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 0", (uint)0x144));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 1", (uint)0x148));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 2", (uint)0x14C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ TABLE 3", (uint)0x150));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 0", (uint)0x154));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 1", (uint)0x158));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 2", (uint)0x15C));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ TABLE 3", (uint)0x160));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 0", (uint)0x164));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 1", (uint)0x168));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 2", (uint)0x16C));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ TABLE 3", (uint)0x170));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 0", (uint)0x174));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 1", (uint)0x178));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 2", (uint)0x17C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK VOLTAGE 3", (uint)0x180));
                    Sensors.Add(CreatePowerMonitoringItem("CPU SET VOLTAGE", (uint)0x184));
                    Sensors.Add(CreatePowerMonitoringItem("CPU TELEMETRY VOLTAGE", (uint)0x188));
                    Sensors.Add(CreatePowerMonitoringItem("CPU TELEMETRY CURRENT", (uint)0x18C));
                    Sensors.Add(CreatePowerMonitoringItem("CPU TELEMETRY POWER", (uint)0x190));
                    Sensors.Add(CreatePowerMonitoringItem("SOC SET VOLTAGE", (uint)0x194));
                    Sensors.Add(CreatePowerMonitoringItem("SOC TELEMETRY VOLTAGE", (uint)0x198));
                    Sensors.Add(CreatePowerMonitoringItem("SOC TELEMETRY CURRENT", (uint)0x19C));
                    Sensors.Add(CreatePowerMonitoringItem("SOC TELEMETRY POWER", (uint)0x1A0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail0 voltage", (uint)0x1A4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail1 voltage", (uint)0x1A8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail4 voltage", (uint)0x1AC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail5 voltage", (uint)0x1B0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail6 voltage", (uint)0x1B4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail7 voltage", (uint)0x1B8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail8 voltage", (uint)0x1BC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail9 voltage", (uint)0x1C0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail10 voltage", (uint)0x1C4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail11 voltage", (uint)0x1C8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail12 voltage", (uint)0x1CC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail13 voltage", (uint)0x1D0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail14 voltage", (uint)0x1D4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail15 voltage", (uint)0x1D8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail16 voltage", (uint)0x1DC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail17 voltage", (uint)0x1E0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail18 voltage", (uint)0x1E4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail19 voltage", (uint)0x1E8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail20 voltage", (uint)0x1EC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail21 voltage", (uint)0x1F0));
                    Sensors.Add(CreatePowerMonitoringItem("Rail0 current", (uint)0x1F4));
                    Sensors.Add(CreatePowerMonitoringItem("Rail1 current", (uint)0x1F8));
                    Sensors.Add(CreatePowerMonitoringItem("Rail4 current", (uint)0x1FC));
                    Sensors.Add(CreatePowerMonitoringItem("Rail5 current", (uint)0x200));
                    Sensors.Add(CreatePowerMonitoringItem("Rail6 current", (uint)0x204));
                    Sensors.Add(CreatePowerMonitoringItem("Rail7 current", (uint)0x208));
                    Sensors.Add(CreatePowerMonitoringItem("Rail8 current", (uint)0x20C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail9 current", (uint)0x210));
                    Sensors.Add(CreatePowerMonitoringItem("Rail10 current", (uint)0x214));
                    Sensors.Add(CreatePowerMonitoringItem("Rail11 current", (uint)0x218));
                    Sensors.Add(CreatePowerMonitoringItem("Rail12 current", (uint)0x21C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail13 current", (uint)0x220));
                    Sensors.Add(CreatePowerMonitoringItem("Rail14 current", (uint)0x224));
                    Sensors.Add(CreatePowerMonitoringItem("Rail15 current", (uint)0x228));
                    Sensors.Add(CreatePowerMonitoringItem("Rail16 current", (uint)0x22C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail17 current", (uint)0x230));
                    Sensors.Add(CreatePowerMonitoringItem("Rail18 current", (uint)0x234));
                    Sensors.Add(CreatePowerMonitoringItem("Rail19 current", (uint)0x238));
                    Sensors.Add(CreatePowerMonitoringItem("Rail20 current", (uint)0x23C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail21 current", (uint)0x240));
                    Sensors.Add(CreatePowerMonitoringItem("Rail0 power", (uint)0x244));
                    Sensors.Add(CreatePowerMonitoringItem("Rail1 power", (uint)0x248));
                    Sensors.Add(CreatePowerMonitoringItem("Rail4 power", (uint)0x24C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail5 power", (uint)0x250));
                    Sensors.Add(CreatePowerMonitoringItem("Rail6 power", (uint)0x254));
                    Sensors.Add(CreatePowerMonitoringItem("Rail7 power", (uint)0x258));
                    Sensors.Add(CreatePowerMonitoringItem("Rail8 power", (uint)0x25C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail9 power", (uint)0x260));
                    Sensors.Add(CreatePowerMonitoringItem("Rail10 power", (uint)0x264));
                    Sensors.Add(CreatePowerMonitoringItem("Rail11 power", (uint)0x268));
                    Sensors.Add(CreatePowerMonitoringItem("Rail12 power", (uint)0x26C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail13 power", (uint)0x270));
                    Sensors.Add(CreatePowerMonitoringItem("Rail14 power", (uint)0x274));
                    Sensors.Add(CreatePowerMonitoringItem("Rail15 power", (uint)0x278));
                    Sensors.Add(CreatePowerMonitoringItem("Rail16 power", (uint)0x27C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail17 power", (uint)0x280));
                    Sensors.Add(CreatePowerMonitoringItem("Rail18 power", (uint)0x284));
                    Sensors.Add(CreatePowerMonitoringItem("Rail19 power", (uint)0x288));
                    Sensors.Add(CreatePowerMonitoringItem("Rail20 power", (uint)0x28C));
                    Sensors.Add(CreatePowerMonitoringItem("Rail21 power", (uint)0x290));
                    Sensors.Add(CreatePowerMonitoringItem("DfBusy", (uint)0x294));
                    Sensors.Add(CreatePowerMonitoringItem("VcnBusy", (uint)0x298));
                    Sensors.Add(CreatePowerMonitoringItem("IohcBusy", (uint)0x29C));
                    Sensors.Add(CreatePowerMonitoringItem("MmhubBusy", (uint)0x2A0));
                    Sensors.Add(CreatePowerMonitoringItem("AthubBusy", (uint)0x2A4));
                    Sensors.Add(CreatePowerMonitoringItem("OsssysBusy", (uint)0x2A8));
                    Sensors.Add(CreatePowerMonitoringItem("HdpBusy", (uint)0x2AC));
                    Sensors.Add(CreatePowerMonitoringItem("SdmaBusy", (uint)0x2B0));
                    Sensors.Add(CreatePowerMonitoringItem("ShubBusy", (uint)0x2B4));
                    Sensors.Add(CreatePowerMonitoringItem("BifBusy", (uint)0x2B8));
                    Sensors.Add(CreatePowerMonitoringItem("AcpBusy", (uint)0x2BC));
                    Sensors.Add(CreatePowerMonitoringItem("Sst0Busy", (uint)0x2C0));
                    Sensors.Add(CreatePowerMonitoringItem("Sst1Busy", (uint)0x2C4));
                    Sensors.Add(CreatePowerMonitoringItem("Usb0Busy", (uint)0x2C8));
                    Sensors.Add(CreatePowerMonitoringItem("Usb1Busy", (uint)0x2CC));
                    Sensors.Add(CreatePowerMonitoringItem("CCM Reads", (uint)0x2D0));
                    Sensors.Add(CreatePowerMonitoringItem("CCM Writes", (uint)0x2D4));
                    Sensors.Add(CreatePowerMonitoringItem("GCM 64B Reads", (uint)0x2D8));
                    Sensors.Add(CreatePowerMonitoringItem("GCM 64B Writes", (uint)0x2DC));
                    Sensors.Add(CreatePowerMonitoringItem("GCM 32B ReadsWrites", (uint)0x2E0));
                    Sensors.Add(CreatePowerMonitoringItem("MMHUB Reads", (uint)0x2E4));
                    Sensors.Add(CreatePowerMonitoringItem("MMHUB Writes", (uint)0x2E8));
                    Sensors.Add(CreatePowerMonitoringItem("DCE Reads", (uint)0x2EC));
                    Sensors.Add(CreatePowerMonitoringItem("IO ReadsWrites", (uint)0x2F0));
                    Sensors.Add(CreatePowerMonitoringItem("CS Reads", (uint)0x2F4));
                    Sensors.Add(CreatePowerMonitoringItem("CS Writes", (uint)0x2F8));
                    Sensors.Add(CreatePowerMonitoringItem("MaxDramBW", (uint)0x2FC));
                    Sensors.Add(CreatePowerMonitoringItem("VCN Busy", (uint)0x300));
                    Sensors.Add(CreatePowerMonitoringItem("VCN Decode", (uint)0x304));
                    Sensors.Add(CreatePowerMonitoringItem("VCN Encode Gen", (uint)0x308));
                    Sensors.Add(CreatePowerMonitoringItem("VCN Encode Low", (uint)0x30C));
                    Sensors.Add(CreatePowerMonitoringItem("VCN Encode Real", (uint)0x310));
                    Sensors.Add(CreatePowerMonitoringItem("VCN PG", (uint)0x314));
                    Sensors.Add(CreatePowerMonitoringItem("VCN JPEG", (uint)0x318));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 0", (uint)0x31C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 1", (uint)0x320));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 2", (uint)0x324));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 3", (uint)0x328));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 4", (uint)0x32C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 5", (uint)0x330));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 6", (uint)0x334));
                    Sensors.Add(CreatePowerMonitoringItem("CORE POWER 7", (uint)0x338));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 0", (uint)0x33C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 1", (uint)0x340));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 2", (uint)0x344));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 3", (uint)0x348));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 4", (uint)0x34C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 5", (uint)0x350));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 6", (uint)0x354));
                    Sensors.Add(CreatePowerMonitoringItem("CORE VOLTAGE 7", (uint)0x358));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 0", (uint)0x35C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 1", (uint)0x360));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 2", (uint)0x364));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 3", (uint)0x368));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 4", (uint)0x36C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 5", (uint)0x370));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 6", (uint)0x374));
                    Sensors.Add(CreatePowerMonitoringItem("CORE TEMP 7", (uint)0x378));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 0", (uint)0x37C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 1", (uint)0x380));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 2", (uint)0x384));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 3", (uint)0x388));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 4", (uint)0x38C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 5", (uint)0x390));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 6", (uint)0x394));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FIT 7", (uint)0x398));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 0", (uint)0x39C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 1", (uint)0x3A0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 2", (uint)0x3A4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 3", (uint)0x3A8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 4", (uint)0x3AC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 5", (uint)0x3B0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 6", (uint)0x3B4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE IDDMAX 7", (uint)0x3B8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 0", (uint)0x3BC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 1", (uint)0x3C0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 2", (uint)0x3C4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 3", (uint)0x3C8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 4", (uint)0x3CC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 5", (uint)0x3D0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 6", (uint)0x3D4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQ 7", (uint)0x3D8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 0", (uint)0x3DC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 1", (uint)0x3E0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 2", (uint)0x3E4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 3", (uint)0x3E8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 4", (uint)0x3EC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 5", (uint)0x3F0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 6", (uint)0x3F4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE FREQEFF 7", (uint)0x3F8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 0", (uint)0x3FC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 1", (uint)0x400));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 2", (uint)0x404));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 3", (uint)0x408));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 4", (uint)0x40C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 5", (uint)0x410));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 6", (uint)0x414));
                    Sensors.Add(CreatePowerMonitoringItem("CORE C0 7", (uint)0x418));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 0", (uint)0x41C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 1", (uint)0x420));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 2", (uint)0x424));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 3", (uint)0x428));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 4", (uint)0x42C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 5", (uint)0x430));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 6", (uint)0x434));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC1 7", (uint)0x438));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 0", (uint)0x43C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 1", (uint)0x440));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 2", (uint)0x444));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 3", (uint)0x448));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 4", (uint)0x44C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 5", (uint)0x450));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 6", (uint)0x454));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CC6 7", (uint)0x458));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 0", (uint)0x45C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 1", (uint)0x460));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 2", (uint)0x464));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 3", (uint)0x468));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 4", (uint)0x46C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 5", (uint)0x470));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 6", (uint)0x474));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CKS FDD 7", (uint)0x478));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 0", (uint)0x47C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 1", (uint)0x480));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 2", (uint)0x484));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 3", (uint)0x488));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 4", (uint)0x48C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 5", (uint)0x490));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 6", (uint)0x494));
                    Sensors.Add(CreatePowerMonitoringItem("CORE PSTATE 7", (uint)0x498));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 0", (uint)0x49C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 1", (uint)0x4A0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 2", (uint)0x4A4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 3", (uint)0x4A8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 4", (uint)0x4AC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 5", (uint)0x4B0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 6", (uint)0x4B4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MAX 7", (uint)0x4B8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 0", (uint)0x4BC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 1", (uint)0x4C0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 2", (uint)0x4C4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 3", (uint)0x4C8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 4", (uint)0x4CC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 5", (uint)0x4D0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 6", (uint)0x4D4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC MIN 7", (uint)0x4D8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 0", (uint)0x4DC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 1", (uint)0x4E0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 2", (uint)0x4E4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 3", (uint)0x4E8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 4", (uint)0x4EC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 5", (uint)0x4F0));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 6", (uint)0x4F4));
                    Sensors.Add(CreatePowerMonitoringItem("CORE CPPC EPP 7", (uint)0x4F8));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 0", (uint)0x4FC));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 1", (uint)0x500));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 2", (uint)0x504));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 3", (uint)0x508));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 4", (uint)0x50C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 5", (uint)0x510));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 6", (uint)0x514));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC LIMIT 7", (uint)0x518));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 0", (uint)0x51C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 1", (uint)0x520));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 2", (uint)0x524));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 3", (uint)0x528));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 4", (uint)0x52C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 5", (uint)0x530));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 6", (uint)0x534));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC CALC 7", (uint)0x538));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 0", (uint)0x53C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 1", (uint)0x540));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 2", (uint)0x544));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 3", (uint)0x548));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 4", (uint)0x54C));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 5", (uint)0x550));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 6", (uint)0x554));
                    Sensors.Add(CreatePowerMonitoringItem("CORE SC RESIDENCY 7", (uint)0x558));
                    Sensors.Add(CreatePowerMonitoringItem("L3 LOGIC POWER 0", (uint)0x55C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 LOGIC POWER 1", (uint)0x560));
                    Sensors.Add(CreatePowerMonitoringItem("L3 VDDM POWER 0", (uint)0x564));
                    Sensors.Add(CreatePowerMonitoringItem("L3 VDDM POWER 1", (uint)0x568));
                    Sensors.Add(CreatePowerMonitoringItem("L3 TEMP 0", (uint)0x56C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 TEMP 1", (uint)0x570));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FIT 0", (uint)0x574));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FIT 1", (uint)0x578));
                    Sensors.Add(CreatePowerMonitoringItem("L3 IDDMAX 0", (uint)0x57C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 IDDMAX 1", (uint)0x580));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FREQ 0", (uint)0x584));
                    Sensors.Add(CreatePowerMonitoringItem("L3 FREQ 1", (uint)0x588));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CKS FDD 0", (uint)0x58C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CKS FDD 1", (uint)0x590));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA THRESHOLD 0", (uint)0x594));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA THRESHOLD 1", (uint)0x598));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA CAC 0", (uint)0x59C));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA CAC 1", (uint)0x5A0));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA ACTIVATION 0", (uint)0x5A4));
                    Sensors.Add(CreatePowerMonitoringItem("L3 CCA ACTIVATION 1", (uint)0x5A8));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC LIMIT 0", (uint)0x5AC));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC LIMIT 1", (uint)0x5B0));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC CAC 0", (uint)0x5B4));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC CAC 1", (uint)0x5B8));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC RESIDENCY 0", (uint)0x5BC));
                    Sensors.Add(CreatePowerMonitoringItem("L3 EDC RESIDENCY 1", (uint)0x5C0));
                    Sensors.Add(CreatePowerMonitoringItem("GFX VOLTAGE", (uint)0x5C4));
                    Sensors.Add(CreatePowerMonitoringItem("GFX TEMP", (uint)0x5C8));
                    Sensors.Add(CreatePowerMonitoringItem("GFX IDDMAX", (uint)0x5CC));
                    Sensors.Add(CreatePowerMonitoringItem("GFX FREQ", (uint)0x5D0));
                    Sensors.Add(CreatePowerMonitoringItem("GFX FREQEFF", (uint)0x5D4));
                    Sensors.Add(CreatePowerMonitoringItem("GFX BUSY", (uint)0x5D8));
                    Sensors.Add(CreatePowerMonitoringItem("GFX CGPG", (uint)0x5DC));
                    Sensors.Add(CreatePowerMonitoringItem("GFX EDC LIMIT", (uint)0x5E0));
                    Sensors.Add(CreatePowerMonitoringItem("GFX EDC RESIDENCY", (uint)0x5E4));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQ", (uint)0x5E8));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQ", (uint)0x5EC));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQ", (uint)0x5F0));
                    Sensors.Add(CreatePowerMonitoringItem("VCLK FREQ", (uint)0x5F4));
                    Sensors.Add(CreatePowerMonitoringItem("DCLK FREQ", (uint)0x5F8));
                    Sensors.Add(CreatePowerMonitoringItem("SOCCLK FREQ", (uint)0x5FC));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK FREQ", (uint)0x600));
                    Sensors.Add(CreatePowerMonitoringItem("SHUBCLK FREQ", (uint)0x604));
                    Sensors.Add(CreatePowerMonitoringItem("MP0CLK FREQ", (uint)0x608));
                    Sensors.Add(CreatePowerMonitoringItem("DCFCLK FREQ", (uint)0x60C));
                    Sensors.Add(CreatePowerMonitoringItem("FCLK FREQEFF", (uint)0x610));
                    Sensors.Add(CreatePowerMonitoringItem("UCLK FREQEFF", (uint)0x614));
                    Sensors.Add(CreatePowerMonitoringItem("MEMCLK FREQEFF", (uint)0x618));
                    Sensors.Add(CreatePowerMonitoringItem("VCLK FREQEFF", (uint)0x61C));
                    Sensors.Add(CreatePowerMonitoringItem("DCLK FREQEFF", (uint)0x620));
                    Sensors.Add(CreatePowerMonitoringItem("SOCCLK FREQEFF", (uint)0x624));
                    Sensors.Add(CreatePowerMonitoringItem("LCLK FREQEFF", (uint)0x628));
                    Sensors.Add(CreatePowerMonitoringItem("SHUBCLK FREQEFF", (uint)0x62C));
                    Sensors.Add(CreatePowerMonitoringItem("MP0CLK FREQEFF", (uint)0x630));
                    Sensors.Add(CreatePowerMonitoringItem("DCFCLK FREQEFF", (uint)0x634));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 0", (uint)0x638));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 1", (uint)0x63C));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 2", (uint)0x640));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 3", (uint)0x644));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 4", (uint)0x648));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 5", (uint)0x64C));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 6", (uint)0x650));
                    Sensors.Add(CreatePowerMonitoringItem("Vclk state freq 7", (uint)0x654));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 0", (uint)0x658));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 1", (uint)0x65C));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 2", (uint)0x660));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 3", (uint)0x664));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 4", (uint)0x668));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 5", (uint)0x66C));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 6", (uint)0x670));
                    Sensors.Add(CreatePowerMonitoringItem("Dclk state freq 7", (uint)0x674));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 0", (uint)0x678));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 1", (uint)0x67C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 2", (uint)0x680));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 3", (uint)0x684));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 4", (uint)0x688));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 5", (uint)0x68C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 6", (uint)0x690));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state freq 7", (uint)0x694));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 0", (uint)0x698));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 1", (uint)0x69C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 2", (uint)0x6A0));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 3", (uint)0x6A4));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 4", (uint)0x6A8));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 5", (uint)0x6AC));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 6", (uint)0x6B0));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state freq 7", (uint)0x6B4));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 0", (uint)0x6B8));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 1", (uint)0x6BC));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 2", (uint)0x6C0));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 3", (uint)0x6C4));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 4", (uint)0x6C8));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 5", (uint)0x6CC));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 6", (uint)0x6D0));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state freq 7", (uint)0x6D4));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 0", (uint)0x6D8));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 1", (uint)0x6DC));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 2", (uint)0x6E0));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 3", (uint)0x6E4));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 4", (uint)0x6E8));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 5", (uint)0x6EC));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 6", (uint)0x6F0));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state freq 7", (uint)0x6F4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 0", (uint)0x6F8));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 1", (uint)0x6FC));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 2", (uint)0x700));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 3", (uint)0x704));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 4", (uint)0x708));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 5", (uint)0x70C));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 6", (uint)0x710));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state freq 7", (uint)0x714));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 0", (uint)0x718));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 1", (uint)0x71C));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 2", (uint)0x720));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 3", (uint)0x724));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 4", (uint)0x728));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 5", (uint)0x72C));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 6", (uint)0x730));
                    Sensors.Add(CreatePowerMonitoringItem("Vcn state residency 7", (uint)0x734));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 0", (uint)0x738));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 1", (uint)0x73C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 2", (uint)0x740));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 3", (uint)0x744));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 4", (uint)0x748));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 5", (uint)0x74C));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 6", (uint)0x750));
                    Sensors.Add(CreatePowerMonitoringItem("Socclk state residency 7", (uint)0x754));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 0", (uint)0x758));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 1", (uint)0x75C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 2", (uint)0x760));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 3", (uint)0x764));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 4", (uint)0x768));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 5", (uint)0x76C));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 6", (uint)0x770));
                    Sensors.Add(CreatePowerMonitoringItem("Lclk state residency 7", (uint)0x774));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 0", (uint)0x778));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 1", (uint)0x77C));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 2", (uint)0x780));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 3", (uint)0x784));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 4", (uint)0x788));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 5", (uint)0x78C));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 6", (uint)0x790));
                    Sensors.Add(CreatePowerMonitoringItem("Shubclk state residency 7", (uint)0x794));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 0", (uint)0x798));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 1", (uint)0x79C));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 2", (uint)0x7A0));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 3", (uint)0x7A4));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 4", (uint)0x7A8));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 5", (uint)0x7AC));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 6", (uint)0x7B0));
                    Sensors.Add(CreatePowerMonitoringItem("Mp0clk state residency 7", (uint)0x7B4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 0", (uint)0x7B8));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 1", (uint)0x7BC));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 2", (uint)0x7C0));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 3", (uint)0x7C4));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 4", (uint)0x7C8));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 5", (uint)0x7CC));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 6", (uint)0x7D0));
                    Sensors.Add(CreatePowerMonitoringItem("Dcfclk state residency 7", (uint)0x7D4));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 0", (uint)0x7D8));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 1", (uint)0x7DC));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 2", (uint)0x7E0));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 3", (uint)0x7E4));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 4", (uint)0x7E8));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 5", (uint)0x7EC));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 6", (uint)0x7F0));
                    Sensors.Add(CreatePowerMonitoringItem("VddcrSoc voltage 7", (uint)0x7F4));
                    Sensors.Add(CreatePowerMonitoringItem("CPUOFF", (uint)0x7F8));
                    Sensors.Add(CreatePowerMonitoringItem("CPUOFF count", (uint)0x7FC));
                    Sensors.Add(CreatePowerMonitoringItem("GFXOFF", (uint)0x800));
                    Sensors.Add(CreatePowerMonitoringItem("GFXOFF count", (uint)0x804));
                    Sensors.Add(CreatePowerMonitoringItem("VDDOFF", (uint)0x808));
                    Sensors.Add(CreatePowerMonitoringItem("VDDOFF count", (uint)0x80C));
                    Sensors.Add(CreatePowerMonitoringItem("ULV", (uint)0x810));
                    Sensors.Add(CreatePowerMonitoringItem("ULV count", (uint)0x814));
                    Sensors.Add(CreatePowerMonitoringItem("S0i2", (uint)0x818));
                    Sensors.Add(CreatePowerMonitoringItem("S0i2 count", (uint)0x81C));
                    Sensors.Add(CreatePowerMonitoringItem("WhisperMode", (uint)0x820));
                    Sensors.Add(CreatePowerMonitoringItem("WhisperMode count", (uint)0x824));
                    Sensors.Add(CreatePowerMonitoringItem("SelfRefresh0", (uint)0x828));
                    Sensors.Add(CreatePowerMonitoringItem("SelfRefresh1", (uint)0x82C));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 0", (uint)0x830));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 1", (uint)0x834));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 2", (uint)0x838));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 3", (uint)0x83C));
                    Sensors.Add(CreatePowerMonitoringItem("Pll power down 4", (uint)0x840));
                    Sensors.Add(CreatePowerMonitoringItem("UINT8 T POWER SOURCE (4)", (uint)0x844));
                    Sensors.Add(CreatePowerMonitoringItem("dGPU POWER", (uint)0x848));
                    Sensors.Add(CreatePowerMonitoringItem("dGPU GFX BUSY", (uint)0x84C));
                    Sensors.Add(CreatePowerMonitoringItem("dGPU FREQ TARGET", (uint)0x850));
                    Sensors.Add(CreatePowerMonitoringItem("V VDDM", (uint)0x854));
                    Sensors.Add(CreatePowerMonitoringItem("V VDDP", (uint)0x858));
                    Sensors.Add(CreatePowerMonitoringItem("DDR PHY POWER", (uint)0x85C));
                    Sensors.Add(CreatePowerMonitoringItem("IO VDDIO MEM POWER", (uint)0x860));
                    Sensors.Add(CreatePowerMonitoringItem("IO VDD18 POWER", (uint)0x864));
                    Sensors.Add(CreatePowerMonitoringItem("IO DISPLAY POWER", (uint)0x868));
                    Sensors.Add(CreatePowerMonitoringItem("IO USB POWER", (uint)0x86C));
                    Sensors.Add(CreatePowerMonitoringItem("ULV VOLTAGE", (uint)0x870));
                    Sensors.Add(CreatePowerMonitoringItem("PEAK TEMP", (uint)0x874));
                    Sensors.Add(CreatePowerMonitoringItem("PEAK VOLTAGE", (uint)0x878));
                    Sensors.Add(CreatePowerMonitoringItem("AVG CORE COUNT", (uint)0x87C));
                    Sensors.Add(CreatePowerMonitoringItem("MAX VOLTAGE", (uint)0x880));
                    Sensors.Add(CreatePowerMonitoringItem("DC BTC", (uint)0x884));
                    Sensors.Add(CreatePowerMonitoringItem("CSTATE BOOST", (uint)0x888));
                    Sensors.Add(CreatePowerMonitoringItem("PROCHOT", (uint)0x88C));
                    Sensors.Add(CreatePowerMonitoringItem("PWM", (uint)0x890));
                    Sensors.Add(CreatePowerMonitoringItem("FPS", (uint)0x894));
                    Sensors.Add(CreatePowerMonitoringItem("DISPLAY COUNT", (uint)0x898));
                    Sensors.Add(CreatePowerMonitoringItem("StapmTimeConstant", (uint)0x89C));
                    Sensors.Add(CreatePowerMonitoringItem("SlowPPTTimeConstant", (uint)0x8A0));
                    Sensors.Add(CreatePowerMonitoringItem("MP1CLK", (uint)0x8A4));
                    Sensors.Add(CreatePowerMonitoringItem("MP2CLK", (uint)0x8A8));
                    Sensors.Add(CreatePowerMonitoringItem("SMNCLK", (uint)0x8AC));
                    Sensors.Add(CreatePowerMonitoringItem("ACLK", (uint)0x8B0));
                    Sensors.Add(CreatePowerMonitoringItem("DISPCLK", (uint)0x8B4));
                    Sensors.Add(CreatePowerMonitoringItem("DPREFCLK", (uint)0x8B8));
                    Sensors.Add(CreatePowerMonitoringItem("DPPCLK", (uint)0x8BC));
                    Sensors.Add(CreatePowerMonitoringItem("SMU BUSY", (uint)0x8C0));
                    Sensors.Add(CreatePowerMonitoringItem("SMU SKIP COUNTER", (uint)0x8C4));
                    break;

            }
            CpuData.Update();
            
        }
        
        private void RefreshData()
        {
            RyzenAccess.Initialize();
            Args = new uint[] { 0, 0, 0, 0, 0, 0 };
            RyzenAccess.SendPsmu(0x65, ref Args);
            int Index = 0;
            float CurrentValue = 0.0F;
            UInt32 CurrentValueUInt = 0;
            //string TestPrint = "";
            foreach (var item in Sensors)
            {
                CurrentValue = ReadFloat(Address, OffsetTable[Index]);
                Sensors[Index].Value = $"{CurrentValue:F4}";
                //Thread.Sleep(10);
                //TestPrint += (Sensors[Index].Value + Environment.NewLine);
                Index++;
            }
            CpuData.Refresh();
            RyzenAccess.Deinitialize();
            //MessageBox.Show(TestPrint);

        }

        private void cpuData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
        {

        }
        
    }


}
