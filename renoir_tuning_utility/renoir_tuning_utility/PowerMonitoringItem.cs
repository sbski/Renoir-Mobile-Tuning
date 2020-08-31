using System;
using System.Runtime.InteropServices;

namespace renoir_tuning_utility
{
    public class PowerMonitoringItem
    {
        public static uint Address { get; set; }
        [DllImport("inpoutx64.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysLong(UIntPtr memAddress, out uint Data);

        public string Offset { get; set; }
        public string Sensor { get; set; }
        public string Value { get; set; }
        public PowerMonitoringItem(string SensorName, uint NewOffset)
        {
            Offset = $"{NewOffset,3:X3}";
            Sensor = SensorName;
            Value = $"{ReadFloat(Address, NewOffset):F}";
        }

        private static float ReadFloat(uint Address, uint Offset)
        {
            uint Data = 0;
            GetPhysLong((UIntPtr)(Address + Offset * 4), out Data);

            byte[] bytes = new byte[4];
            bytes = BitConverter.GetBytes(Data);

            float PmData = BitConverter.ToSingle(bytes, 0);
            //Console.WriteLine($"0x{Address + Offset * 4,8:X8} | {PmData:F}");
            return PmData;
        }


    }


}
