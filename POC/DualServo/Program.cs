using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace DualServo
{
    public class Program
    {
        public static void Main()
        {
            using (var servoLeft = new PWM(PWMChannels.PWM_PIN_D5, 20000, 1500, PWM.ScaleFactor.Microseconds, false))
            using (var servoRight = new PWM(PWMChannels.PWM_PIN_D10, 20000, 1500, PWM.ScaleFactor.Microseconds, false))
            {                
                servoLeft.Start();
                servoRight.Start();
                                
                //TODO Map degree to duration                
                servoLeft.Duration = 5000; // 90 degree left      
                servoRight.Duration = 5000;
                Thread.Sleep(5000);

                servoLeft.Stop();
                servoRight.Stop();
            }  
        }

    }
}
