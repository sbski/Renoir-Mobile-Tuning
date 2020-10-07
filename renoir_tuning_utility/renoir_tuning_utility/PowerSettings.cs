using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RyzenSmu;
namespace PowerSettings
{
    public class PmTableVersion
    {
        public UInt32 TableVersion { get; set; }
        public UInt32 TctlTempOffset { get; set; }
        public UInt32 StapmLimitOffset { get; set; }
        public UInt32 SlowLimitOffset { get; set; }
        public UInt32 StapmTimeOffset { get; set; }
        public UInt32 CurrentLimitOffset { get; set; }
        public UInt32 SlowTimeOffset { get; set; }
        public UInt32 FastLimitOffset { get; set; }
        public UInt32 MaxCurrentLimitOffset { get; set; }
        public UInt32 GfxClkOffset { get; set; }

    }

    public class PowerSetting
    {
        public String Name { get; set; }
        public UInt32 SettingsVersion { get; protected set; }
        
        public UInt32 TctlTemp { get; set; }
        public UInt32 StapmLimit { get; set; }
        public UInt32 SlowTime { get; set; }
        public UInt32 StapmTime { get; set; }
        public UInt32 CurrentLimit { get; set; }
        public UInt32 SlowLimit { get; set; }
        public UInt32 FastLimit { get; set; }
        public UInt32 MaxCurrentLimit { get; set; }
        public UInt32 GfxClk { get; set; }
        public bool SmartReapply { get; set; }

        public PmTableVersion TableInfo;

        public PowerSetting( UInt32 TableVersion)
        {
            Name = "";
            SettingsVersion = 1;

            FastLimit = 0;
            SlowLimit = 0;
            StapmLimit = 0;
            SlowTime = 0;
            StapmTime = 0;
            
            TctlTemp = 0;
            CurrentLimit = 0;
            MaxCurrentLimit = 0;

            TableInfo = new PmTableVersion();
            TableInfo.TableVersion = TableVersion;
            if(!Init())
            {
                Name = "invalid";
            }

        }

        private bool Init()
        {
            TableInfo.StapmLimitOffset = 0x0;
            TableInfo.FastLimitOffset = 0x2;
            TableInfo.SlowLimitOffset = 0x4;
            TableInfo.CurrentLimitOffset = 0x8;
            TableInfo.MaxCurrentLimitOffset = 0xC;
            TableInfo.TctlTempOffset = 0x10;
            switch (TableInfo.TableVersion)
            {
                case (UInt32)0x00370004:
                    TableInfo.StapmTimeOffset = 0x220;
                    TableInfo.SlowTimeOffset = 0x221;
                    break;
                case (UInt32)0x00370005:
                    TableInfo.StapmTimeOffset = 0x227;
                    TableInfo.SlowTimeOffset = 0x228;
                    break;
                default:
                    return false;
            }
            return true;
        }
        public bool CheckAndReapply(UInt32 Address)
        {
            Smu RyzenAccess = new Smu(false);

            RyzenAccess.Initialize();

            //String exe = Directory.GetCurrentDirectory() + "\\smu-tool\\smu-tool.exe";

            int i = 0;
            Smu.Status[] Statuses = new Smu.Status[8];
            uint[] Args = new uint[6];
            uint Msg = 0x0;

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.StapmLimitOffset)) != StapmLimit)
            {
                //Set Msg and Args
                Msg = 0x14;
                Args[0] = StapmLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.FastLimitOffset)) != FastLimit)
            {
                //Set Msg and Args
                Msg = 0x15;
                Args[0] = FastLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.SlowLimitOffset)) != SlowLimit)
            {
                //Set Msg and Args
                Msg = 0x16;
                Args[0] = SlowLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.SlowTimeOffset)) != SlowTime)
            {
                //Set Msg and Args
                Msg = 0x17;
                Args[0] = SlowTime;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.StapmTimeOffset)) != StapmTime)
            {
                //Set Msg and Args
                Msg = 0x18;
                Args[0] = StapmTime;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.TctlTempOffset)) != TctlTemp)
            {
                //Set Msg and Args
                Msg = 0x19;
                Args[0] = TctlTemp;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.CurrentLimitOffset)) != CurrentLimit)
            {
                //Set Msg and Args
                Msg = 0x1A;
                Args[0] = CurrentLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (Convert.ToUInt32((float)Smu.ReadFloat(Address, TableInfo.MaxCurrentLimitOffset)) != MaxCurrentLimit)
            {
                //Set Msg and Args
                Msg = 0x1C;
                Args[0] = MaxCurrentLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
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
            if (i != 0)
            {

                //Set Msg and Args
                Msg = 0xB9;
                Args[0] = GfxClk;

                //Send msg
                Statuses[i++] = RyzenAccess.SendPsmu(Msg, ref Args);
            }
            for (int j = 0; j < i; j++)
            {
                if (Statuses[j] != Smu.Status.OK)
                {
                    //MessageBox.Show($"{j:D}-Status: " + Statuses[j].ToString());
                }
            }
            return true;
        }
        public bool ApplySettings()
        {
            Smu RyzenAccess = new Smu(false);

            RyzenAccess.Initialize();

            //String exe = Directory.GetCurrentDirectory() + "\\smu-tool\\smu-tool.exe";

            int i = 0;
            Smu.Status[] Statuses = new Smu.Status[8];
            uint[] Args = new uint[6];
            uint Msg = 0x0;

            if (0 != StapmLimit)
            {
                //Set Msg and Args
                Msg = 0x14;
                Args[0] = StapmLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != FastLimit)
            {
                //Set Msg and Args
                Msg = 0x15;
                Args[0] = FastLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != SlowLimit)
            {
                //Set Msg and Args
                Msg = 0x16;
                Args[0] = SlowLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != SlowTime)
            {
                //Set Msg and Args
                Msg = 0x17;
                Args[0] = SlowTime;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != StapmTime)
            {
                //Set Msg and Args
                Msg = 0x18;
                Args[0] = StapmTime;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != TctlTemp)
            {
                //Set Msg and Args
                Msg = 0x19;
                Args[0] = TctlTemp;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != CurrentLimit)
            {
                //Set Msg and Args
                Msg = 0x1A;
                Args[0] = CurrentLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }

            if (0 != MaxCurrentLimit)
            {
                //Set Msg and Args
                Msg = 0x1C;
                Args[0] = MaxCurrentLimit;

                //Send msg
                Statuses[i++] = RyzenAccess.SendMp1(Msg, ref Args);
            }
            
            for (int j = 0; j < i; j++)
            {
                if (Statuses[j] != Smu.Status.OK)
                {
                    //MessageBox.Show($"{j:D}-Status: " + Statuses[j].ToString());
                    return false;
                }
            }

            return true;
        }
    }
    
}
