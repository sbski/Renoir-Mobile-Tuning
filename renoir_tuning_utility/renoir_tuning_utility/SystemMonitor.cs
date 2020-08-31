using RyzenSmu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace renoir_tuning_utility
{



    public partial class SystemMonitor : Form
    {
       

        private static Smu RyzenAccess;
        private static uint Address;
        private static uint[] Args;

        
        private void FillInData(uint[] table)
        {

            
        }

        public SystemMonitor()
        {
            RyzenAccess = new Smu();
            Args = new uint[6];
            RyzenAccess.SendPsmu(0x66, ref Args);
            Address = Args[0];
            InitializeComponent();
            PowerMonitoringItem.Address = Address;
        }



       


        private void buttonApply_Click(object sender, EventArgs e)
        {
            sampleTimer.Interval = (int)numericUpDownInterval.Value;
        }

        private void sampleTimer_Tick(object sender, EventArgs e)
        {
            UpdatePMTable();
        }
        private void UpdatePMTable()
        {

            List<PowerMonitoringItem> Sensors = new List<PowerMonitoringItem>();
            Sensors.Add(new PowerMonitoringItem("STAPM Limit", 0x000));
            Sensors.Add(new PowerMonitoringItem("STAPM Power", 0x004));
            Sensors.Add(new PowerMonitoringItem("Fast Limit", 0x008));
            Sensors.Add(new PowerMonitoringItem("Current Power", 0x00C));
            Sensors.Add(new PowerMonitoringItem("Slow Limit", 0x010));
            Sensors.Add(new PowerMonitoringItem("Slow Power", 0x014));
            Sensors.Add(new PowerMonitoringItem("EDC Limit", 0x020));
            Sensors.Add(new PowerMonitoringItem("EDC Used", 0x024));
            Sensors.Add(new PowerMonitoringItem("Soc EDC Limit", 0x028));
            Sensors.Add(new PowerMonitoringItem("Soc EDC Used", 0x02C));
            Sensors.Add(new PowerMonitoringItem("TDC Limit", 0x030));
            Sensors.Add(new PowerMonitoringItem("TDC Used", 0x034));
            Sensors.Add(new PowerMonitoringItem("Soc TDC Limit", 0x038));
            Sensors.Add(new PowerMonitoringItem("Soc TDC Used", 0x03C));
            Sensors.Add(new PowerMonitoringItem("Core Temperature", 0x044));
            Sensors.Add(new PowerMonitoringItem("Gfx Temperature", 0x04C));
            Sensors.Add(new PowerMonitoringItem("Soc Temperature", 0x054));
            Sensors.Add(new PowerMonitoringItem("GPU  Frequency", 0x5B4));
            cpuData.DataSource = Sensors;

        }

        private void cpuData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
        {

        }
        
    }


}
