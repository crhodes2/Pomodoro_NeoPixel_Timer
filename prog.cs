void main(string[] args)
{
	SerialPort port = new SerialPort(SerialPort.GetPortNames()[0]);
	port.BaudRate = 115200;
	port.Open();
	
	Console.ReadLine();
	
	byte[] data = new byte[]
	{
		0xAA,
		1,
		0,0,0,0
	};
	UInt32 color = (your color of choice);
	byte[] colorData = BitConverter.GetBytes(color);
	Array.Copy(colorData, 0, data, 2, colorData.Length);
	
	port.Write(data, 0, data.Length);
	Console.WriteLine("data written");
	Console.ReadLine();
	port.Close();
}