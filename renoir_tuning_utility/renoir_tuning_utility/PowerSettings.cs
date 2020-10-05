using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSettings
{
    public class PowerSetting
    {
        public String Name { get; set; }
        public UInt32 TctlTemp { get; set; }
        public UInt32 StapmLimit { get; set; }
        public UInt32 SlowTime { get; set; }
        public UInt32 StapmTime { get; set; }
        public UInt32 CurrentLimit { get; set; }
        public UInt32 SlowLimit { get; set; }
        public UInt32 FastLimit { get; set; }
        public UInt32 MaxCurrentLimit { get; set; }

        public PowerSetting()
        {
            Name = "";
            TctlTemp = 0;
            StapmLimit = 0;
            SlowLimit = 0;
            StapmTime = 0;
            CurrentLimit = 0;
            SlowLimit = 0;
            FastLimit = 0;
            MaxCurrentLimit = 0;
        }
    }
}
