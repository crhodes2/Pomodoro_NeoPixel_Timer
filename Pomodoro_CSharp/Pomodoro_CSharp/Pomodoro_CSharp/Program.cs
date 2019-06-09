using System;
using System.IO.Ports;


namespace Pomodoro_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quitIt = false;
            string userColor;
            SerialPort port = new SerialPort(SerialPort.GetPortNames()[0]);
            string userPixel;

            port.BaudRate = 115200;
            port.Open();

            // PROGRAM STARTS HERE WITH A TITLE SCREEN //
            Console.WriteLine("Pomodoro Color Changer ~ Type quit anytime to quit\n ...or press enter to proceed.");
            string userinput = Console.ReadLine();

            /* if user had not quit the program, go to an infinite loop */
            if (userinput != "quit")
            {
                while (!quitIt)
                {
                    /* user chooses a pin of their choice*/
                    Console.WriteLine("Choose an option representing the range between 10 mins - 60 mins");
                    Console.WriteLine("1 - 10 mins (Light Blue)\n2 - 20 mins (Green Jade)\n3 - 30 mins (Sun Gold)\n4 - 40 mins (Hot Pink)\n" +
                        "5 - 50 mins (Ruby Red)\n6 - 60 mins (Purple Rain)\n");
                    userPixel = Console.ReadLine();

                    /* if user had typed quit in the program at anytime, close port and quit program */
                    if (userPixel == "quit")
                    {
                        port.Close();
                        Console.WriteLine("Close the program...");
                        System.Environment.Exit(1);
                    }

                    /* user chooses a color of choice */
                    Console.WriteLine("\nType a color");
                    Console.WriteLine("red | salmon | crimson | green | turquoise | emerald | blue | navy | cobalt | \n " +
                        "sky blue | cyan | yellow | orange | brown | magenta | purple | indigo | white\n");
                    userColor = Console.ReadLine();

                    /* if user had typed quit in the program at anytime, close port and quit program */
                    if (userPixel == "quit")
                    {
                        port.Close();
                        Console.WriteLine("Close the program...");
                        System.Environment.Exit(1);
                    }

                    /* set up the pixel and color data in int and in byte */
                    int pixel = Convert.ToInt32(userPixel);
                    byte[] data = new byte[]
                    {
                        0xAA,
                        (byte)pixel,
                        0,0,0,0
                    };
                    UInt32 color = 0x00000000;

                    /* program select the user's color of choice in a switch statement and transfers it to Adafruit 
                     if the color is missing or not found, automatically revert back to Supergirl */
                    switch (userColor)
                    {
                        case "red":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00FF0000;
                            break;
                        case "salmon":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00C67171;
                            break;
                        case "crimson":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00DC143C;
                            break;
                        case "green":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x0000FF00;
                            break;
                        case "turquoise":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x0040E0D0;
                            break;
                        case "emerald":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x0000C957;
                            break;
                        case "blue":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x000000FF;
                            break;
                        case "navy":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00000080;
                            break;
                        case "cobalt":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x003D59AB;
                            break;
                        case "sky blue":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x087CEFF;
                            break;
                        case "cyan":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x0000FFFF;
                            break;
                        case "yellow":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00FFFF00;
                            break;
                        case "orange":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00FFA500;
                            break;
                        case "brown":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x008B7765;
                            break;
                        case "magenta":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00FF00FF;
                            break;
                        case "purple":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00800080;
                            break;
                        case "indigo":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x004B0082;
                            break;
                        case "white":
                            Console.WriteLine("Success! Color changed to " + userColor);
                            color = 0x00FFFFFF;
                            break;
                        case "quit":
                            port.Close();
                            Console.WriteLine("Close the program...");
                            System.Environment.Exit(1);
                            break;
                        default:
                            // no change. Keep original colors intact
                            Console.WriteLine("Sorry: Color Not found.\n Returning to original color...");
                            switch (userPixel)
                            {
                                case "1":
                                    color = 0x0008080;
                                    break;
                                case "2":
                                    color = 0x033FF33;
                                    break;
                                case "3":
                                    color = 0x0FFD700;
                                    break;
                                case "4":
                                    color = 0x0FF2F2F;
                                    break;
                                case "5":
                                    color = 0x0FF0101;
                                    break;
                                case "6":
                                    color = 0x04B0082;
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }

                    /* write the color to Adafruit automatically */
                    byte[] colordata = BitConverter.GetBytes(color);
                    Array.Copy(colordata, 0, data, 2, colordata.Length);

                    /* display the chosen color to Adafruit */
                    port.Write(data, 0, data.Length);
                    Console.WriteLine("\n");

                    /* Go through the loop again, unless user types in quit */
                }
            }
            else
            {
                /* quit program */
                port.Close();
                Console.WriteLine("Close the program...");
                System.Environment.Exit(1);
            }

        }

    }
}
