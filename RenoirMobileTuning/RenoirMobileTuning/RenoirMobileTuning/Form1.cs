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

namespace RenoirMobileTuning
{
    public partial class rmtForm : Form
    {
        private readonly Ols rmtOls;
        readonly Mutex hMutexPci;

        private uint SMU_ADDR_MSG;
        private uint SMU_ADDR_RSP;
        private uint SMU_ADDR_ARG;

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
            switch (ols.GetStatus())
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
            switch (ols.GetDllStatus())
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

    }
}
