using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RyzenSmu;

namespace RyzenSMUBackend
{
    internal class Addresses
    {
        

        public static void SetAddresses()
        {
            int FAMID = Families.FAMID;

            RyzenSmu.Smu.SMU_PCI_ADDR = 0x00000000;
            RyzenSmu.Smu.SMU_OFFSET_ADDR = 0xB8;
            RyzenSmu.Smu.SMU_OFFSET_DATA = 0xBC;


            if(FAMID == 0 || FAMID == 1 || FAMID == 2 || FAMID == 3 || FAMID == 7)
            {
                RyzenSmu.Smu.MP1_ADDR_MSG = 0x03B10528;
                RyzenSmu.Smu.MP1_ADDR_RSP = 0x03B10564;
                RyzenSmu.Smu.MP1_ADDR_ARG = 0x03B10998;

                RyzenSmu.Smu.PSMU_ADDR_MSG = 0x03B10A20;
                RyzenSmu.Smu.PSMU_ADDR_RSP = 0x03B10A80;
                RyzenSmu.Smu.PSMU_ADDR_ARG = 0x03B10A88;
            }
            else if (FAMID == 5 || FAMID == 8)
            {
                RyzenSmu.Smu.MP1_ADDR_MSG = 0x03B10528;
                RyzenSmu.Smu.MP1_ADDR_RSP = 0x03B10564;
                RyzenSmu.Smu.MP1_ADDR_ARG = 0x03B10998;

                RyzenSmu.Smu.PSMU_ADDR_MSG = 0x03B10A20;
                RyzenSmu.Smu.PSMU_ADDR_RSP = 0x03B10A80;
                RyzenSmu.Smu.PSMU_ADDR_ARG = 0x03B10A88;
            }
            else if (FAMID == 4 || FAMID == 6)
            {
                RyzenSmu.Smu.MP1_ADDR_MSG = 0x3B10530;
                RyzenSmu.Smu.MP1_ADDR_RSP = 0x3B1057C;
                RyzenSmu.Smu.MP1_ADDR_ARG = 0x3B109C4;

                RyzenSmu.Smu.PSMU_ADDR_MSG = 0x3B10524;
                RyzenSmu.Smu.PSMU_ADDR_RSP = 0x3B10570;
                RyzenSmu.Smu.PSMU_ADDR_ARG = 0x3B10A40;
            }
            else
            {
                RyzenSmu.Smu.MP1_ADDR_MSG = 0;
                RyzenSmu.Smu.MP1_ADDR_RSP = 0;
                RyzenSmu.Smu.MP1_ADDR_ARG = 0;

                RyzenSmu.Smu.PSMU_ADDR_MSG = 0;
                RyzenSmu.Smu.PSMU_ADDR_RSP = 0;
                RyzenSmu.Smu.PSMU_ADDR_ARG = 0;
            }
        } 
    }
}
