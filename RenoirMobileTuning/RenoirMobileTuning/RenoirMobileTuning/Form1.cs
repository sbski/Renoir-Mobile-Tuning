using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenLibSys;
using System.Threading;
using ZenStatesDebugTool;

namespace RenoirMobileTuning
{
    public partial class rmtForm : Form
    {
        

        private readonly Ols rmtOls;
        readonly Mutex hMutexPci;
        private SMU smu;
        private SystemInfo SI;
        private int _coreCount;

        private const uint SMU_ADDR_MSG = 0x03B10528;
        private const uint SMU_ADDR_RSP = 0x03B10564;
        private const uint SMU_ADDR_ARG = 0x03B10998;

        private const uint SMU_PCI_ADDR = 0x00000000;
        private const uint SMU_OFFSET_ADDR = 0xB8;
        private const uint SMU_OFFSET_DATA = 0xBC;

        public rmtForm()
        {
            InitializeComponent();

            rmtOls = new Ols();
            hMutexPci = new Mutex();

            try
            {
                CheckOlsStatus();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                Dispose();
                Application.Exit();
            }
        }

        private void CheckOlsStatus()
        {
            // Check support library status
            switch (rmtOls.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    throw new ApplicationException("WinRing DLL_NOT_FOUND");
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    throw new ApplicationException("WinRing DLL_INCORRECT_VERSION");
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    throw new ApplicationException("WinRing DLL_INITIALIZE_ERROR");
            }

            // Check WinRing0 status
            switch (rmtOls.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }
        }

        private bool SmuWriteReg(uint addr, uint data)
        {
            if (ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_ADDR, addr) == 1)
            {
                return ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_DATA, data) == 1;
            }
            return false;
        }

        private bool SmuReadReg(uint addr, ref uint data)
        {
            if (ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_ADDR, addr) == 1)
            {
                return ols.ReadPciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_DATA, ref data) == 1;
            }
            return false;
        }

        private bool SmuWaitDone()
        {
            bool res = false;
            ushort timeout = 1000;
            uint data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
                res = SmuReadReg(SMU_ADDR_RSP, ref data);
            }

            if (timeout == 0 || data != 1) res = false;

            return res;
        }

        private bool SmuRead(uint msg, ref uint data)
        {
            if (SmuWriteReg(SMU_ADDR_RSP, 0))
            {
                if (SmuWriteReg(SMU_ADDR_MSG, msg))
                {
                    if (SmuWaitDone())
                    {
                        return SmuReadReg(SMU_ADDR_ARG, ref data);
                    }
                }
            }

            return false;
        }

        private bool SmuWrite(uint msg, uint value)
        {
            bool res = false;
            // Mutex
            if (hMutexPci.WaitOne(5000))
            {
                // Clear response
                if (SmuWriteReg(SMU_ADDR_RSP, 0))
                {
                    // Write data
                    if (SmuWriteReg(SMU_ADDR_ARG, value))
                    {
                        SmuWriteReg(SMU_ADDR_ARG + 4, 0);
                    }

                    // Send message
                    if (SmuWriteReg(SMU_ADDR_MSG, msg))
                    {
                        res = SmuWaitDone();
                    }
                }
            }

            hMutexPci.ReleaseMutex();
            return res;
        }

        private uint ReadDword(uint value)
        {
            ols.WritePciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_ADDR, value);
            return ols.ReadPciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_DATA);
        }

        

        private bool IsProchotEnabled()
        {
            uint data = ReadDword(0x59804);
            return (data & 1) == 1;
        }

        private void SetStatusText(string status)
        {
            //.Text = status;
            Console.WriteLine($"CMD Status: {status}");
        }

        private uint GetSmuVersion()
        {
            uint version = 0;
            if (SmuRead(smu.SMC_MSG_GetSmuVersion, ref version))
            {
                return version;
            }
            return 0;
        }

    }
}
