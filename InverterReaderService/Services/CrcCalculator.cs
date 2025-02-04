using System.Text;

namespace InverterReaderService.Services
{
    public class CrcCalculator
    {
        public static string CalculateCrc(string command)
        {
            var commandBytes = Encoding.ASCII.GetBytes(command);
            ushort crc = ComputeCrc(commandBytes);
            string crcHex = crc.ToString("X4");
            return command + crcHex + "\r";
        }

        private static ushort ComputeCrc(byte[] data)
        {
            const ushort polynomial = 0x1021;
            ushort crc = 0;

            foreach (byte b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ polynomial);
                    else
                        crc <<= 1;
                }
            }

            return crc;
        }
    }
}