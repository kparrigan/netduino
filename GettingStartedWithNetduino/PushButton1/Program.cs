using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace PushButton1
{
    public class Program
    {
        public static void Main()
        {
            var led = new OutputPort(Pins.ONBOARD_LED, false);
            var button = new InputPort(Pins.ONBOARD_BTN, false, Port.ResistorMode.Disabled);
            var buttonState = false;

            while(true)
            {
                buttonState = button.Read();
                led.Write(buttonState);
            }
        }

    }
}
