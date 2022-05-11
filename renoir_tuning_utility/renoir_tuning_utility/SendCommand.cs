using RyzenSmu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyzenSMUBackend
{
    internal class SendCommand
    {

        //RAVEN - 0
        //PICASSO - 1
        //DALI - 2
        //RENOIR - 3
        //MATISSE - 4
        //VANGOGH - 5
        //VERMEER - 6
        //CEZANNE - 7
        //REMBRANDT - 8
        //PHEONIX - 9
        //RAPHAEL - 10

        public static Smu RyzenAccess = new Smu(false);
        public static int FAMID = Families.FAMID;


        //STAMP Limit
        public static void set_stapm_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x1a, ref Args);
            }
            else if (FAMID != 4 && FAMID != 6 && FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x14, ref Args);
                RyzenAccess.SendPsmu(0x31, ref Args);
            }
        }

        //Fast Limit
        public static void set_fast_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x1b, ref Args);
            }
            else if (FAMID != 4 && FAMID != 6 && FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x15, ref Args);
            }
        }

        //Slow Limit
        public static void set_slow_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x1c, ref Args);
            }
            else if (FAMID != 4 && FAMID != 6 && FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x16, ref Args);
            }
        }

        //Slow time
        public static void set_slow_time(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x1d, ref Args);
            }
            else if (FAMID != 4 && FAMID != 6 && FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x17, ref Args);
            }
        }

        //STAMP Time
        public static void set_stapm_time(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x1e, ref Args);
            }
            else if (FAMID != 4 && FAMID != 6 && FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x18, ref Args);
            }
        }

        //TCTL Temp Limit
        public static void set_tctl_temp(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x1f, ref Args);
            }
            else if (FAMID != 4 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x19, ref Args);
            }
        }

        //Skim Temp limit
        public static void set_apu_skin_temp_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID == 5 || FAMID == 8)
            {
                RyzenAccess.SendMp1(0x33, ref Args);
            }
            else if (FAMID != 4 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x38, ref Args);
            }
        }

        //VRM Current
        public static void set_vrm_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x20, ref Args);
            }
            else if (FAMID != 4 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x1a, ref Args);
            }
        }

        //VRM SoC Current
        public static void set_vrmsoc_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x21, ref Args);
            }
            else if (FAMID != 4 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x1b, ref Args);
            }
        }

        //VRM GFX Current
        public static void set_vrmgfx_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID == 5)
            {
                RyzenAccess.SendMp1(0x1c, ref Args);
            }
        }

        //VRM CVIP Current
        public static void set_vrmcvip_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID == 5)
            {
                RyzenAccess.SendMp1(0x1d, ref Args);
            }
        }

        //VRM Max Current
        public static void set_vrmmax_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x22, ref Args);
            }
            else if (FAMID == 5)
            {
                RyzenAccess.SendMp1(0x1e, ref Args);
            }
            else if (FAMID != 4 || FAMID != 5 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x1c, ref Args);
            }
        }

        //VRM GFX Max Current
        public static void set_vrmgfxmax_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID == 5)
            {
                RyzenAccess.SendMp1(0x1f, ref Args);
            }
        }

        //VRM SoC Max Current
        public static void set_vrmsocmax_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x23, ref Args);
            }
            else if (FAMID != 4 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x1d, ref Args);
            }
        }

        //GFX Clock Max
        public static void set_max_gfxclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x46, ref Args);
            }
        }

        //GFX Clock Min
        public static void set_min_gfxclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x47, ref Args);
            }
        }

        //SoC Clock Max
        public static void set_max_socclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x48, ref Args);
            }
        }

        //SoC Clock Min
        public static void set_min_socclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x49, ref Args);
            }
        }

        //FCLK Clock Max
        public static void set_max_fclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x4a, ref Args);
            }
        }

        //FCLK Clock Min
        public static void set_min_fclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x4b, ref Args);
            }
        }

        //VCN Clock Max
        public static void set_max_vcn_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x4c, ref Args);
            }
        }

        //VCN Clock Min
        public static void set_min_vcn_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x4d, ref Args);
            }
        }

        //LCLK Clock Max
        public static void set_max_lclk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x4e, ref Args);
            }
        }

        //LCLK Clock Min
        public static void set_min_lclk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x4f, ref Args);
            }
        }

        //Prochot Ramp
        public static void set_prochot_deassertion_ramp(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID < 3)
            {
                RyzenAccess.SendMp1(0x26, ref Args);
            }
            else if (FAMID == 5)
            {
                RyzenAccess.SendMp1(0x22, ref Args);
            }
            else if (FAMID == 8)
            {
                RyzenAccess.SendMp1(0x1f, ref Args);
            }
            else if (FAMID != 4 || FAMID != 6 || FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x20, ref Args);
            }
        }

        //GFX Clock
        public static void set_gfx_clk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID == 8)
            {
                RyzenAccess.SendPsmu(0x1c, ref Args);
            }
            else if (FAMID == 3)
            {
                RyzenAccess.SendPsmu(0x89, ref Args);
            }
        }

        //SttLimit
        public static void set_sst_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            if (FAMID !< 3 && FAMID != 4 && FAMID != 6 && FAMID != 99999)
            {
                RyzenAccess.SendMp1(0x38, ref Args);
            }
        }
    }
}
