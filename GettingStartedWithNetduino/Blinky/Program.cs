using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Blinky
{
    public class Program
    {
        public static void Main()
        {
            const int sleep = 250;
            var led = new OutputPort(Pins.ONBOARD_LED, false);

            while(true)
            {
                led.Write(true);
                Thread.Sleep(sleep);
                led.Write(false);
                Thread.Sleep(sleep);
            }

        }

    }
}
