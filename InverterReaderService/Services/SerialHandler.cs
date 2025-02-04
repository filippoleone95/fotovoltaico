using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace InverterReaderService.Services
{
    public class SerialHandler
    {
        private readonly SerialPort _serialPort;

        public SerialHandler(string portName, int baudRate, int timeout)
        {
            _serialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = baudRate,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                ReadTimeout = timeout,
                WriteTimeout = timeout,
                Handshake = Handshake.None
            };
        }

        public void Open()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public string ReadData()
        {
            StringBuilder responseBuilder = new StringBuilder();
            try
            {
                while (true)
                {
                    // Leggi dati disponibili dalla porta seriale
                    string chunk = _serialPort.ReadExisting();

                    if (!string.IsNullOrEmpty(chunk))
                    {
                        responseBuilder.Append(chunk);
                    }

                    // Controlla se la risposta è completa (rileva il carattere di fine '\r')
                    if (responseBuilder.ToString().Contains('\r'))
                    {
                        break;
                    }
                }
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Timeout durante la lettura della risposta.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la lettura dei dati: {ex.Message}");
            }

            return responseBuilder.ToString().Trim(); // Rimuovi eventuali spazi o caratteri extra
        }

        public string SendCommand(string command)
        {
            try
            {
                // Calcola il CRC per il comando
                byte[] commandWithCrc = CalculateCrc(command);

                Open();

                // Scrive il comando con il CRC
                _serialPort.Write(commandWithCrc, 0, commandWithCrc.Length);
                Thread.Sleep(500); // Simula il ritardo per ricevere i dati

                // Leggi la risposta iterativamente
                return ReadData();
                // Legge la risposta
                //byte[] buffer = new byte[256];
                //int bytesRead = _serialPort.Read(buffer, 0, buffer.Length);

                //if (bytesRead > 0)
                //{
                //    return Encoding.GetEncoding("ISO-8859-1").GetString(buffer, 0, bytesRead).Trim();
                //}

                //return "Timeout: Nessuna risposta ricevuta.";
            }
            catch (TimeoutException)
            {
                return "Timeout: Nessuna risposta ricevuta.";
            }
            catch (Exception ex)
            {
                return $"Errore: {ex.Message}";
            }
            finally
            {
                Close();
            }
        }

        private byte[] CalculateCrc(string command)
        {
            byte[] commandBytes = Encoding.ASCII.GetBytes(command);
            ushort crc = ComputeCrc16Xmodem(commandBytes);

            // Aggiunge CRC e terminatore '\r'
            byte[] commandWithCrc = new byte[commandBytes.Length + 3];
            Array.Copy(commandBytes, commandWithCrc, commandBytes.Length);
            commandWithCrc[commandBytes.Length] = (byte)((crc >> 8) & 0xFF);       // CRC High byte
            commandWithCrc[commandBytes.Length + 1] = (byte)(crc & 0xFF);          // CRC Low byte
            commandWithCrc[commandBytes.Length + 2] = (byte)'\r';                  // Terminatore

            return commandWithCrc;
        }

        private static ushort ComputeCrc16Xmodem(byte[] data)
        {
            ushort crc = 0x0000; // Valore iniziale CRC
            foreach (var b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021); // Polinomio XMODEM
                    else
                        crc <<= 1;
                }
            }
            return crc;
        }
    }
}