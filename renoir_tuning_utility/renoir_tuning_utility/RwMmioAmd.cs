using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace renoir_tuning_utility
{
    
    internal class RwMmioAmd
    {
        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysLong(UIntPtr memAddress, ref uint DData);

        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsInpOutDriverOpen();

        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetPhysLong(UIntPtr memAddress, ref uint DData);

        readonly int[] bclk_table = { 100, 101, 102, 103, 96, 97, 98, 99, 108, 109, 110, 111, 104, 105, 106, 107, 116, 117, 118, 119,
        112, 113, 114, 115, 124, 125, 126, 127, 120, 121, 122, 123, 132, 133, 134, 135, 128, 129, 130, 131, 140,
        141, 142, 143, 136, 137, 138, 139, 148, 149, 150, 151, 144, 145, 146, 147 };
        uint baseAddress = 0xfed80000;
        const uint MISC_baseAddress = 0xfed80E00;
        const uint SMUIO_baseAddress = 0x0005A000;
        const uint SMBUS_baseAddress = 0xFED80A00;
        const uint UMC0_baseAddress = 0x00050000;
        const uint MiscClkCntl1 = 0x40;
        const uint CGPLLCONFIG1 = 0x8;
        const uint CGPllCfg3 = 0x10;

        uint RdFchMisc(uint regadd, uint startbit, uint endbit)
        {
            baseAddress = MISC_baseAddress;
            UIntPtr p = (UIntPtr)regadd;
            uint pdwPhysVal = 0;
            GetPhysLong(p, ref pdwPhysVal);
            return pdwPhysVal;
            
            //uint value = 0 ;
            
            //value = ((uint)((int)pdwPhysVal >> (int)startbit)) & (max_valuefrombits(endbit - startbit + 1));
            //return value;
        }

        //done
        void WrFchMisc(uint regadd, uint Value, uint startbit, uint endbit)
        {
            baseAddress = MISC_baseAddress;
            UIntPtr p = (UIntPtr)regadd;
            uint pdwPhysVal = 0;
            GetPhysLong(p, ref pdwPhysVal);
            pdwPhysVal = pdwPhysVal & Value;
            pdwPhysVal = SetBit(pdwPhysVal, Value, startbit, endbit);
            SetPhysLong(p, ref pdwPhysVal);
            Cg1CfgUpdateReq();
        }

        uint SetBit(uint pdwPhysVal, uint value, uint startbit, uint endbit)
        {
            uint result = pdwPhysVal;
            uint setValue = value & (uint)1;
            for (int i = (int)startbit; i < endbit + 1; i++)
            {
                uint bitMask = ~((uint)1 << i);
                result = (result & bitMask) | (setValue << i);
            }
            return result;
        }

        void Cg1CfgUpdateReq()
        {
            baseAddress = MISC_baseAddress;
            UIntPtr p = (UIntPtr)MiscClkCntl1;
            uint pdwPhysVal = 0;
            GetPhysLong(p, ref pdwPhysVal);
            pdwPhysVal = SetBit(pdwPhysVal, 1, 30, 30);
            SetPhysLong(p, ref pdwPhysVal);
        }

        internal double GetBclk()
        {
            uint bclk = RdFchMisc(CGPllCfg3, 4, 12);
            uint bclk_decimal = RdFchMisc(CGPllCfg3, 25, 28);
            double bclk_decimal_value = (bclk_decimal) * 0.0625;
            if (bclk >= bclk_table.Length)
            {
                return 151 + bclk_decimal_value;
            }
            int i;
            for (i = 0; i < bclk_table.Length; i++)
            {
                if (bclk == i)
                    break;
            }
            double bclk2 = bclk_table[i];
            bclk2 += bclk_decimal_value;
            return bclk2;
        }
        internal void SetBclk(double BCLK)
        {
            DisableSsc();
            Cg1FbdivLoadEn();
            CG1PllFcw0IntOverride(BCLK);
        }

        void CG1PllFcw0IntOverride(double BCLK)
        {
            WrFchMisc(CGPllCfg3, calculate_bclk(Convert.ToInt32(BCLK)), 4, 12);
            int int_portion = (int)BCLK;
            double remainder = BCLK - int_portion;
            if (remainder > 0.0)
            {
                int number_of_units = (int)(remainder / 0.0625);
                WrFchMisc(CGPllCfg3, (uint)number_of_units, 25, 28);
            }
            else
            {
                WrFchMisc(CGPllCfg3, 0, 25, 28);
            }
        }

        //done
        uint calculate_bclk(int bclk)
        {
            if (bclk < 96)
                bclk = 96;
            if (bclk > 151)
                bclk = 151;
            uint i = 0;
            for (i = 0; i < bclk_table.Length; i++)
            {
                if (bclk == bclk_table[i])
                {
                    return i;
                }
            }
            return i;
        }

        //done
        void DisableSsc()
        {
            WrFchMisc(CGPLLCONFIG1,0,0,0);
        }

        void Cg1FbdivLoadEn()
        {
            WrFchMisc(MiscClkCntl1, 1, 25, 25);
        }
    }
}
