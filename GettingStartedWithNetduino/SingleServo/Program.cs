using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace SingleServo
{
    public class Program
    {
        public static void Main()
        {
            using(var servo = new PWM(PWMChannels.PWM_PIN_D5, 20000, 1500, PWM.ScaleFactor.Microseconds, false))
            {
                servo.Start();       

                servo.Duration = 5000; // Full Right            
                Thread.Sleep(500);
                servo.Duration = 1000; // Full Left            
                Thread.Sleep(500);
                servo.Duration = 2500; // Full Center            
                Thread.Sleep(500);

                servo.Stop();
            }            
        }

    }
}
