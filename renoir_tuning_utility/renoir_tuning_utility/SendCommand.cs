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
        //RENOIR/LUCIENNE - 3
        //MATISSE - 4
        //VANGOGH - 5
        //VERMEER - 6
        //CEZANNE/BARCELO - 7
        //REMBRANDT - 8
        //PHEONIX - 9
        //RAPHAEL/DRAGON RANGE - 10

        public static Smu RyzenAccess = new Smu(false);
        public static int FAMID = Families.FAMID;


        //STAMP Limit
        public static void set_stapm_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x1a, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x14, ref Args);
                    RyzenAccess.SendPsmu(0x31, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Fast Limit
        public static void set_fast_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;
            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x1b, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x15, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Slow Limit
        public static void set_slow_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x1c, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x16, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Slow time
        public static void set_slow_time(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x1d, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x17, ref Args);
                    break;
                default:
                    break;
            }
        }

        //STAMP Time
        public static void set_stapm_time(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x1e, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x18, ref Args);
                    break;
                default:
                    break;
            }
        }

        //TCTL Temp Limit
        public static void set_tctl_temp(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x1f, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x19, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Skin Temp limit
        public static void set_apu_skin_temp_limit(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 5:
                case 8:
                    RyzenAccess.SendMp1(0x33, ref Args);
                    break;
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x38, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VRM Current
        public static void set_vrm_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x20, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x1a, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VRM SoC Current
        public static void set_vrmsoc_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x21, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x1b, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VRM GFX Current
        public static void set_vrmgfx_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 5:
                    RyzenAccess.SendMp1(0x1c, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VRM CVIP Current
        public static void set_vrmcvip_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 5:
                    RyzenAccess.SendMp1(0x1d, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VRM Max Current
        public static void set_vrmmax_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x22, ref Args);
                    break;
                case 5:
                    RyzenAccess.SendMp1(0x1e, ref Args);
                    break;
                case 3:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x1c, ref Args);
                    break;
                default:
                    break;
            }

        }

        //VRM GFX Max Current
        public static void set_vrmgfxmax_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 5:
                    RyzenAccess.SendMp1(0x1f, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VRM SoC Max Current
        public static void set_vrmsocmax_current(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x23, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x1d, ref Args);
                    break;
                default:
                    break;
            }
        }

        //GFX Clock Max
        public static void set_max_gfxclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x46, ref Args);
                    break;
                default:
                    break;
            }
        }

        //GFX Clock Min
        public static void set_min_gfxclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x47, ref Args);
                    break;
                default:
                    break;
            }
        }

        //SoC Clock Max
        public static void set_max_socclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x48, ref Args);
                    break;
                default:
                    break;
            }
        }

        //SoC Clock Min
        public static void set_min_socclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x49, ref Args);
                    break;
                default:
                    break;
            }
        }

        //FCLK Clock Max
        public static void set_max_fclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x4a, ref Args);
                    break;
                default:
                    break;
            }
        }

        //FCLK Clock Min
        public static void set_min_fclk_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x4b, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VCN Clock Max
        public static void set_max_vcn_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x4c, ref Args);
                    break;
                default:
                    break;
            }
        }

        //VCN Clock Min
        public static void set_min_vcn_freq(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x4d, ref Args);
                    break;
                default:
                    break;
            }
        }

        //LCLK Clock Max
        public static void set_max_lclk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x4e, ref Args);
                    break;
                default:
                    break;
            }
        }

        //LCLK Clock Min
        public static void set_min_lclk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x4f, ref Args);
                    break;
            }
        }

        //Prochot Ramp
        public static void set_prochot_deassertion_ramp(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x26, ref Args);
                    break;
                case 5:
                    RyzenAccess.SendMp1(0x22, ref Args);
                    break;
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x20, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendMp1(0x1f, ref Args);
                    break;
                default:
                    break;
            }
        }

        //GFX Clock
        public static void set_gfx_clk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendPsmu(0x89, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendPsmu(0x1c, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Power Saving
        public static void set_power_saving(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x19, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x12, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Max Performance
        public static void set_max_performance(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 0:
                case 1:
                case 2:
                    RyzenAccess.SendMp1(0x18, ref Args);
                    break;
                case 3:
                case 5:
                case 7:
                case 8:
                    RyzenAccess.SendMp1(0x11, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set All Core OC
        public static void set_oc_clk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x31, ref Args);
                    RyzenAccess.SendPsmu(0x19, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x26, ref Args);
                    RyzenAccess.SendPsmu(0x5c, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendPsmu(0x19, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set Per Core OC
        public static void set_per_core_oc_clk(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x32, ref Args);
                    RyzenAccess.SendPsmu(0x1a, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x27, ref Args);
                    RyzenAccess.SendPsmu(0x5d, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendPsmu(0x1a, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set Per Core OC
        public static void set_oc_volt(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x33, ref Args);
                    RyzenAccess.SendPsmu(0x1b, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x28, ref Args);
                    RyzenAccess.SendPsmu(0x61, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set All Core Curve Optimiser
        public static void set_coall(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x55, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x36, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendMp1(0x4c, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set Per Core Curve Optimiser
        public static void set_coper(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x54, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x35, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendMp1(0x4b, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set iGPU Curve Optimiser
        public static void set_cogfx(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x64, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendPsmu(0xb7, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Disable OC
        public static void set_disable_oc()
        {
            uint value = 0x0;
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x30, ref Args);
                    RyzenAccess.SendPsmu(0x1d, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x25, ref Args);
                    RyzenAccess.SendPsmu(0x5b, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendPsmu(0x18, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Enable OC
        public static void set_enable_oc()
        {
            uint value = 0x0;
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 3:
                case 7:
                    RyzenAccess.SendMp1(0x2f, ref Args);
                    RyzenAccess.SendPsmu(0x1d, ref Args);
                    break;
                case 4:
                case 6:
                    RyzenAccess.SendMp1(0x24, ref Args);
                    RyzenAccess.SendPsmu(0x5a, ref Args);
                    break;
                case 8:
                    RyzenAccess.SendPsmu(0x17, ref Args);
                    break;
                default:
                    break;
            }
        }


        //Set PPT
        public static void set_ppt(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 4:
                case 6:
                    RyzenAccess.SendPsmu(0x53, ref Args);
                    break;
                default:
                    break;
            }
        }


        //Set TDC
        public static void set_tdc(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 4:
                case 6:
                    RyzenAccess.SendPsmu(0x54, ref Args);
                    break;
                default:
                    break;
            }
        }

        //Set TDC
        public static void set_edc(uint value)
        {
            RyzenAccess.Initialize();
            uint[] Args = new uint[6];
            Args[0] = value;

            switch (FAMID)
            {
                case 4:
                case 6:
                    RyzenAccess.SendPsmu(0x55, ref Args);
                    break;
                default:
                    break;
            }
        }
    }
}
