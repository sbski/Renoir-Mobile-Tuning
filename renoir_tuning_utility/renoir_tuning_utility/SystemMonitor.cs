using Microsoft.Win32;
using RyzenSmu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
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
        private readonly uint[] OffsetTable = { 
            0x000,
            0x004,
            0x008,
            0x00C,
            0x010,
            0x014,
            0x020,
            0x024,
            0x028,
            0x02C,
            0x030,
            0x034,
            0x038,
            0x03C,
            0x044,
            0x04C,
            0x054,
            0x5B4};

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
            RyzenAccess = new Smu();
            Args = new uint[6];
            RyzenAccess.SendPsmu(0x66, ref Args);
            Address = Args[0];
            


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
            
        }

        private void FillInTable()
        {
            
            Sensors.Clear();

            Sensors.Add(CreatePowerMonitoringItem("STAPM Limit", (uint)0x000));
            Sensors.Add(CreatePowerMonitoringItem("STAPM Power", (uint)0x004));
            Sensors.Add(CreatePowerMonitoringItem("Fast Limit", (uint)0x008));
            Sensors.Add(CreatePowerMonitoringItem("Current Power", (uint)0x00C));
            Sensors.Add(CreatePowerMonitoringItem("Slow Limit", (uint)0x010));
            Sensors.Add(CreatePowerMonitoringItem("Slow Power", (uint)0x014));
            Sensors.Add(CreatePowerMonitoringItem("TDC Limit", (uint)0x020));
            Sensors.Add(CreatePowerMonitoringItem("TDC Used", (uint)0x024));
            Sensors.Add(CreatePowerMonitoringItem("Soc TDC Limit", (uint)0x028));
            Sensors.Add(CreatePowerMonitoringItem("Soc TDC Used", (uint)0x02C));
            Sensors.Add(CreatePowerMonitoringItem("EDC Limit", (uint)0x030));
            Sensors.Add(CreatePowerMonitoringItem("EDC Used", (uint)0x034));
            Sensors.Add(CreatePowerMonitoringItem("Soc EDC Limit", (uint)0x038));
            Sensors.Add(CreatePowerMonitoringItem("Soc EDC Used", (uint)0x03C));
            Sensors.Add(CreatePowerMonitoringItem("Core Temperature", (uint)0x044));
            Sensors.Add(CreatePowerMonitoringItem("Gfx Temperature", (uint)0x04C));
            Sensors.Add(CreatePowerMonitoringItem("Soc Temperature", (uint)0x054));
            Sensors.Add(CreatePowerMonitoringItem("GPU  Frequency", (uint)0x5B4));
            CpuData.Update();
            
        }
        
        private void RefreshData()
        {
            RyzenAccess.Initialize();
            Args = new uint[] { 0, 0, 0, 0, 0, 0 };
            RyzenAccess.SendPsmu(0x65, ref Args);
            int Index = 0;
            //string TestPrint = "";
            foreach (var item in Sensors)
            {
                Sensors[Index].Value = $"{ReadFloat(Address, OffsetTable[Index]):F4}";
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
