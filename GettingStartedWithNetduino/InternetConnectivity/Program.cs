using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Net.NetworkInformation;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace InternetConnectivity
{
    public class Program
    {
        public static void Main()
        {
            const int portNumber = 80;
            var led = new OutputPort(Pins.ONBOARD_LED, false);

            // Sleep to give the Netduino time to get an IP.
            Thread.Sleep(5000);

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            if (networkInterfaces != null && networkInterfaces.Length > 0)
            {
                var networkInterface = networkInterfaces[0];
                Debug.Print("Current IP Address: " + networkInterface.IPAddress);

                var listenerSocket = new Socket(AddressFamily.InterNetwork, 
                                                SocketType.Stream, 
                                                ProtocolType.Tcp);
                var listenerEndpoint = new IPEndPoint(IPAddress.Any, portNumber);

                listenerSocket.Bind(listenerEndpoint);
                listenerSocket.Listen(1);
                
                while(true)
                {
                    var clientSocket = listenerSocket.Accept();

                    try
                    {
                        bool dataReady = clientSocket.Poll(5000000, SelectMode.SelectRead);

                        if (dataReady && clientSocket.Available > 0)
                        {
                            var buffer = new byte[clientSocket.Available];
                            int bytesRead = clientSocket.Receive(buffer);
                            var request = new String(Encoding.UTF8.GetChars(buffer));

                            if (request.IndexOf("ON") >= 0)
                            {
                                led.Write(true);
                            }
                            else if (request.IndexOf("OFF") >= 0)
                            {
                                led.Write(false);
                            }

                            var statusText = "LED is " + (led.Read() ? "ON" : "OFF");
                            var response = "HTTP/1.1 200 OK\r\n" +
                                           "Content-Type: text/html; charset=utf-8\r\n\r\n" +
                                           "<html><head><title>Netduino Plus LED Sample</title></head>" +
                                           "<body>" + statusText + "</body></html>";

                            Debug.Print(statusText);
                            clientSocket.Send(Encoding.UTF8.GetBytes(response));
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                    finally
                    {                        
                        clientSocket.Close();
                    }
                }
            }
            else
            {
                Debug.Print("No network interfaces found.");
            }
        }

    }
}
