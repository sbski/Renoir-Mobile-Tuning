using System;
using System.Collections.Generic;
using System.Text;
using OpenLibSys;

namespace RyzenInterface
{
    class RyzenInterface
    {
        public RyzenSmu smu { get; protected set; }
        
        RyzenInterface()
        {

        }
    }

    public enum MailBoxType : int
    {
        UNDEFINED = 0x0,
        MP0,
        MP1,
        PSMU
    }

    public class MailBox
    {


        //Mailbox Address
        public uint MB_ADDR_MSG { get; protected set; }
        public uint MB_ADDR_RSP { get; protected set; }
        public uint MB_ADDR_ARG { get; protected set; }

        public MailBoxType type { get; protected set; }

        public MailBox()
        {
            type = MailBoxType.UNDEFINED;
            MB_ADDR_MSG = 0x0;
            MB_ADDR_RSP = 0x0;
            MB_ADDR_ARG = 0x0;
        }

        public MailBox(MailBoxType setType, uint msg, uint rsp, uint arg)
        {
            type = setType;
            MB_ADDR_MSG = msg;
            MB_ADDR_RSP = rsp;
            MB_ADDR_ARG = arg;
        }
    }

    public abstract class RyzenSmu
    {
        public enum CPUType : int
        {
            Unsupported = 0,
            DEBUG,
            SummitRidge,
            Threadripper,
            RavenRidge,
            PinnacleRidge,
            Picasso,
            Fenghuang,
            Matisse,
            Rome,
            Renoir
        }

        public enum Status : int
        {
            OK = 0x1,
            FAILED = 0xFF,
            UNKNOWN_CMD = 0xFE,
            CMD_REJECTED_PREREQ = 0xFD,
            CMD_REJECTED_BUSY = 0xFC
        }


        public MailBox PM0 { get; protected set; }
        public MailBox PM1 { get; protected set; }
        public MailBox PSMU { get; protected set; }


        public RyzenSmu()
        {
            SMU_PCI_ADDR = 0x00000000;
            SMU_OFFSET_ADDR = 0xB8;
            SMU_OFFSET_DATA = 0xBC;
        }

        public uint SMU_PCI_ADDR { get; protected set; }
        public uint SMU_OFFSET_ADDR { get; protected set; }
        public uint SMU_OFFSET_DATA { get; protected set; }

        public bool sendmessage
    }



    public class RavenRidgeMobile : RyzenSmu
    {
        RavenRidgeMobile()
        {
            PM0 = new MailBox(MailBoxType.UNDEFINED, 0x0, 0x0, 0x0);
            PM1 = new MailBox(MailBoxType.MP1, 0x03B10528, 0x03B10564, 0x03B10998);
            PSMU = new MailBox(MailBoxType.PSMU, 0x03B10A20, 0x03B10A80, 0x03B10A88);
        }
    }

    public class RavenRidge2Mobile : RyzenSmu
    {
        RavenRidge2Mobile()
        {
            PM0 = new MailBox(MailBoxType.UNDEFINED, 0x0, 0x0, 0x0);
            PM1 = new MailBox(MailBoxType.MP1, 0x03B10528, 0x03B10564, 0x03B10998);
            PSMU = new MailBox(MailBoxType.PSMU, 0x03B10A20, 0x03B10A80, 0x03B10A88);
        }
    }

    public class PicassoMobile : RyzenSmu
    {
        PicassoMobile()
        {
            PM0 = new MailBox(MailBoxType.UNDEFINED, 0x0, 0x0, 0x0);
            PM1 = new MailBox(MailBoxType.MP1, 0x03B10528, 0x03B10564, 0x03B10998);
            PSMU = new MailBox(MailBoxType.PSMU, 0x03B10A20, 0x03B10A80, 0x03B10A88);
        }
    }

    public class RenoirMobile : RyzenSmu
    {
        RenoirMobile()
        {
            PM0 = new MailBox(MailBoxType.UNDEFINED, 0x0, 0x0, 0x0);
            PM1 = new MailBox(MailBoxType.MP1, 0x03B10528, 0x03B10564, 0x03B10998);
            PSMU = new MailBox(MailBoxType.PSMU, 0x03B10A20, 0x03B10A80, 0x03B10A88);
        }
    }
}
