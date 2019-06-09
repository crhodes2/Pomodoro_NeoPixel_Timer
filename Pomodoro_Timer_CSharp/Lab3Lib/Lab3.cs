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
        #region Unused Variables

        /****************************************************************************************************************************************************************************/
        /* UNUSED VARIABLES */

        public float TempC => throw new NotImplementedException();
        public float TempF => throw new NotImplementedException();
        public ushort Light => throw new NotImplementedException();
        public ushort Microphone => throw new NotImplementedException();
        public bool LeftButton => throw new NotImplementedException();
        public bool RightButton => throw new NotImplementedException();
        public bool SlideSwitch => throw new NotImplementedException();


        public void ClearAllPixels()
        {
            throw new NotImplementedException();
        }

        public void PlayTone(float frequency, int ms)
        {
            throw new NotImplementedException();
        }

        public void SetLED13(byte brightness)
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

        /****************************************************************************************************************************************************************************/


        #endregion

        /*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                                                                          VARIABLES     
        -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        /****************************************************************************************************************************************************************************/

        public bool IsOpen { get; private set; }
        public AccelData Accel { get; private set; }

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        private SerialPort port = new SerialPort();

        // CONSTRUCTOR =============================================================>
        public Lab3()
        {
            port.BaudRate = 115200;
            port.DtrEnable = true;
            port.ReadTimeout = 50000;
        }

        // METHODS =================================================================>

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

        public void SetPixelColor(int pixel, LedColor color)
        {
            byte[] data =
            {
                0xAA,
                (byte) pixel,
                color.R,
                color.G,
                color.B
            };

            UInt32 myColor = (uint)color.R + (uint)color.G + (uint)color.B;
            byte[] colorData = BitConverter.GetBytes(myColor);
            Array.Copy(colorData, 0, data, (byte)pixel, colorData.Length);
            port.Write(data, 0, data.Length);
        }

        public void Close()
        {
            port.Close();
        }
    }
}