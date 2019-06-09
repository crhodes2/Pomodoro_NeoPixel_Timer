using CircuitPlaygroundLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Lib
{
    public class Lab3 : ICircuitPlayground, ILab3
    {
        public bool IsOpen { get; private set; }

        public AccelData Accel { get; private set; }

        public float TempC => throw new NotImplementedException();

        public float TempF => throw new NotImplementedException();

        public ushort Light => throw new NotImplementedException();

        public ushort Microphone => throw new NotImplementedException();

        public bool LeftButton => throw new NotImplementedException();

        public bool RightButton => throw new NotImplementedException();

        public bool SlideSwitch => throw new NotImplementedException();

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        private SerialPort port = new SerialPort();

        public Lab3()
        {
            port.BaudRate = 115200;
            port.DtrEnable = true;
            port.DataReceived += Port_DataReceived;
            port.ReadTimeout = 1000;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (port.IsOpen)
            {
                try
                {
                    string line = port.ReadLine();

                    string[] parts = line.Split(',');

                    if (parts.Length >= 3)
                    {
                            float x = float.Parse(parts[0]);
                            float y = float.Parse(parts[1]);
                            float z = float.Parse(parts[2]);

                            AccelData accel = new AccelData()
                            {
                                X = x,
                                Y = y,
                                Z = z
                            };

                            Accel = accel;

                            DataReceived?.Invoke(this, new DataReceivedEventArgs() { UpdatedSensors = Sensors.Accel });
                    }
                }
                catch { }
            }
        }

        public void ClearAllPixels()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            port.Close();
        }

        public string[] GetDevices()
        {
            return SerialPort.GetPortNames();
        }

        public bool Open(int device)
        {
            try
            {
                port.PortName = SerialPort.GetPortNames()[device];

                port.Open();
                IsOpen = true;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void PlayTone(float frequency, int ms)
        {
            throw new NotImplementedException();
        }

        public void SetLED13(byte brightness)
        {
            throw new NotImplementedException();
        }

        public void SetPixelColor(int pixel, LedColor color)
        {
            throw new NotImplementedException();
        }

        public void SetPixelCrossFade(int pixel, LedColor start, LedColor end, int fadeTime, bool repeat = false)
        {
            throw new NotImplementedException();
        }

        public bool IsBeingHeld()
        {
            throw new NotImplementedException();
        }

        public void SetPattern(int id)
        {
            throw new NotImplementedException();
        }

        public void ExportSensorData(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
