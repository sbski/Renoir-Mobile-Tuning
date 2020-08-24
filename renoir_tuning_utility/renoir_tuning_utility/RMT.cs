using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RyzenSmu;

namespace renoir_tuning_utility
{
    public partial class RMT : Form
    {
        uint Msg;
        uint[] Args;
        uint Address;
        RyzenSmu.RyzenSmu RyzenAccess;

        private float fastLimit, slowLimit, stapmLimit, slowTime, stapmTime, currentLimit;
        public RMT()
        {
            InitializeComponent();
            fastLimit = -1;
            slowLimit = -1;
            stapmLimit = -1;
            slowTime = -1;
            stapmTime = -1;
            currentLimit = -1;
            upDownTctlTemp.Enabled = false;
            upDownStapmLimit.Enabled = false;
            upDownSlowTime.Enabled = false;
            upDownStapmTime.Enabled = false;
            upDownCurrentLimit.Enabled = false;
            upDownSlowLimit.Enabled = false;
            upDownFastLimit.Enabled = false;
            upDownMaxCurrentLimit.Enabled = false;

            if(true)
            {
                Args = new uint[6];
                RyzenAccess = new RyzenSmu.RyzenSmu();
                RyzenAccess.Initialize();
                if(RyzenAccess.SendPsmu(0x66, ref Args) == RyzenSmu.RyzenSmu.Status.OK)
                {
                    Address = Args[0];
                    Args[0] = 0;
                    if (RyzenAccess.SendPsmu(0x65, ref Args) == RyzenSmu.RyzenSmu.Status.OK)
                    {
                        upDownStapmLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x0));
                        upDownFastLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x2));
                        upDownSlowLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x4));

                        upDownSlowTime.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x221));
                        upDownStapmTime.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x220));

                        upDownTctlTemp.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x10));
                        upDownCurrentLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0x8));
                        upDownMaxCurrentLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0xC));

                        //upDownSocCurrentLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0xA));
                        //upDownSocMaxCurrentLimit.Value = (decimal)(RyzenAccess.ReadFloat(Address, 0xE));

                    }
                }

            }
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
            
        }


        private void checkTctlTemp_CheckedChanged(object sender, EventArgs e)
        {
            upDownTctlTemp.Enabled = checkTctlTemp.Checked;
        }

        private void checkStapmLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownStapmLimit.Enabled = checkStapmLimit.Checked;
        }

        private void checkSlowTime_CheckedChanged(object sender, EventArgs e)
        {
            upDownSlowTime.Enabled = checkSlowTime.Checked;
        }

        private void checkStapmTime_CheckedChanged(object sender, EventArgs e)
        {
            upDownStapmTime.Enabled = checkStapmTime.Checked;
        }

        private void checkCurrentLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownCurrentLimit.Enabled = checkCurrentLimit.Checked;
        }

        private void ApplySettings_Click(object sender, EventArgs e)
        {
            RyzenAccess = new RyzenSmu.RyzenSmu();

            RyzenAccess.Initialize();

            String exe = Directory.GetCurrentDirectory() + "\\smu-tool\\smu-tool.exe";
            
            int i = 0;
            RyzenSmu.RyzenSmu.Status[] Statuses = new RyzenSmu.RyzenSmu.Status[8];
            Args = new uint[6];

            if (checkStapmLimit.Checked)
            {
                //Set Msg and Args
                Msg = 0x14;
                Args[0] = (uint)Convert.ToInt32(upDownStapmLimit.Value * 1000);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x14 --arg0={0:0}000", upDownStapmLimit.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkFastLimit.Checked)
            {
                //Set Msg and Args
                Msg = 0x15;
                Args[0] = (uint)Convert.ToInt32(upDownFastLimit.Value * 1000);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x15 --arg0={0:0}000", upDownFastLimit.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkSlowLimit.Checked)
            {
                //Set Msg and Args
                Msg = 0x16;
                Args[0] = (uint)Convert.ToInt32(upDownSlowLimit.Value * 1000);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x16 --arg0={0:0}000", upDownSlowLimit.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkSlowTime.Checked)
            {
                //Set Msg and Args
                Msg = 0x17;
                Args[0] = (uint)Convert.ToInt32(upDownSlowTime.Value);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x17 --arg0={0:0}", upDownSlowTime.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkStapmTime.Checked)
            {
                //Set Msg and Args
                Msg = 0x18;
                Args[0] = (uint)Convert.ToInt32(upDownStapmTime.Value);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x18 --arg0={0:0}", upDownStapmTime.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkTctlTemp.Checked)
            {
                //Set Msg and Args
                Msg = 0x19;
                Args[0] = (uint)Convert.ToInt32(upDownTctlTemp.Value);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x19 --arg0={0:0}", upDownTctlTemp.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkCurrentLimit.Checked)
            {
                //Set Msg and Args
                Msg = 0x1A;
                Args[0] = (uint)Convert.ToInt32(upDownCurrentLimit.Value * 1000);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x1A --arg0={0:0}000", upDownCurrentLimit.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }
            if (checkMaxCurrentLimit.Checked)
            {
                //Set Msg and Args
                Msg = 0x1C;
                Args[0] = (uint)Convert.ToInt32(upDownMaxCurrentLimit.Value * 1000);

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);

                //args = String.Format("--message=0x1C --arg0={0:0}000", upDownMaxCurrentLimit.Value);
                //var proc = System.Diagnostics.Process.Start(exe, args);
            }
            /*
             * IF "%Stapm%" NEQ "" smu-tool.exe -m --message=0x14 --arg0=%Stapm%000
             * IF "%Fast%" NEQ "" smu-tool.exe -m --message=0x15 --arg0=%Fast%000
             * IF "%Slow%" NEQ "" smu-tool.exe -m --message=0x16 --arg0=%Slow%000
             * IF "%SlowTime%" NEQ "" smu-tool.exe -m --message=0x17 --arg0=%SlowTime%
             * IF "%StapmTime%" NEQ "" smu-tool.exe -m --message=0x18 --arg0=%StapmTime%
             * IF "%Tctl%" NEQ "" smu-tool.exe -m --message=0x19 --arg0=%Tctl%
             * IF "%Current%" NEQ "" smu-tool.exe -m --message=0x1A --arg0=%Current%000
             * IF "%MaxCurrent%" NEQ "" smu-tool.exe -m --message=0x1C --arg0=%MaxCurrent%000
             */

            for(int j = 0; j < i; j++)
            {/*
                if (Statuses[j] != RyzenSmu.RyzenSmu.Status.OK)
                {
                    throw new ApplicationException($"{j:D}-Status: " + Statuses[j].ToString());
                }*/
            }

            RyzenAccess.Deinitialize();

        }

        private void checkMaxCurrentLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownMaxCurrentLimit.Enabled = checkMaxCurrentLimit.Checked;
        }

        private void upDownSlowLimit_ValueChanged(object sender, EventArgs e)
        {
            if (upDownFastLimit.Value < upDownSlowLimit.Value)
            {
                upDownFastLimit.Value = upDownSlowLimit.Value;
            }
            if (upDownSlowLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownSlowLimit.Value;
                upDownStapmLimit_ValueChanged(sender, e);
            }
        }

        private void upDownStapmLimit_ValueChanged(object sender, EventArgs e)
        {
            if (upDownFastLimit.Value < upDownStapmLimit.Value)
            {
                upDownFastLimit.Value = upDownStapmLimit.Value;
            }
            if (upDownSlowLimit.Value < upDownStapmLimit.Value)
            {
                upDownSlowLimit.Value = upDownStapmLimit.Value;
            }
            if (upDownCurrentLimit.Value < upDownStapmLimit.Value)
            {
                upDownCurrentLimit.Value = upDownStapmLimit.Value;
            }
            if (upDownMaxCurrentLimit.Value < upDownStapmLimit.Value + 15)
            {
                upDownMaxCurrentLimit.Value = upDownStapmLimit.Value + 15;
            }
        }

        private void upDownSlowTime_ValueChanged(object sender, EventArgs e)
        {
            if(Math.Round(upDownSlowTime.Value * 2) > upDownStapmTime.Value)
            {
                upDownStapmTime.Value = upDownSlowTime.Value * 2;
            }
        }

        private void upDownStapmTime_ValueChanged(object sender, EventArgs e)
        {
            if (Math.Round(upDownSlowTime.Value * 2) > upDownStapmTime.Value)
            {
                upDownSlowTime.Value = Math.Round(upDownStapmTime.Value / 2);
            }
        }

        private void upDownCurrentLimit_ValueChanged(object sender, EventArgs e)
        {
            if(upDownCurrentLimit.Value + 15 > upDownMaxCurrentLimit.Value)
            {
                upDownMaxCurrentLimit.Value = upDownCurrentLimit.Value + 15;
            }
            if (upDownCurrentLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownCurrentLimit.Value;
            }
        }

        private void upDownMaxCurrentLimit_ValueChanged(object sender, EventArgs e)
        {
            if(upDownCurrentLimit.Value + 15 > upDownMaxCurrentLimit.Value)
            {
                upDownCurrentLimit.Value = upDownMaxCurrentLimit.Value - 15;
            }
            if (upDownCurrentLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownCurrentLimit.Value;
            }
        }

        private void checkMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void notifyIconRMT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIconRMT.Visible = false;
        }

        private void checkSlowLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownSlowLimit.Enabled = checkSlowLimit.Checked;
        }

        private void checkFastLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownFastLimit.Enabled = checkFastLimit.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (upDownFastLimit.Value < upDownSlowLimit.Value)
            {
                upDownSlowLimit.Value = upDownFastLimit.Value;
            }
            if (upDownFastLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownFastLimit.Value;
                upDownStapmLimit_ValueChanged(sender, e);
            }
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
