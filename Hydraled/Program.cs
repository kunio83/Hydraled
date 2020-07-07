using System;
using System.Diagnostics;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace Hydraled
{
    class Program
    {
        static void Main(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("waiting for debugger attach");
                if (Debugger.IsAttached) break;
                System.Threading.Thread.Sleep(1000);
            }


            Console.WriteLine("Hello World!");

            // GPIO 17 which is physical pin 11
            int ledPin1 = 17;
            int ledPin2 = 27;
            int ledPin3 = 22;
            GpioController controller = new GpioController();
            // Sets the pin to output mode so we can switch something on
            controller.OpenPin(ledPin1, PinMode.Output);
            controller.OpenPin(ledPin2, PinMode.Output);
            controller.OpenPin(ledPin3, PinMode.Output);

            int lightTimeInMilliseconds = 1000;
            int dimTimeInMilliseconds = 200;

            while (true)
            {
                Console.WriteLine($"LED1 on for {lightTimeInMilliseconds}ms");
                // turn on the LED
                controller.Write(ledPin1, PinValue.Low);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"LED1 off for {dimTimeInMilliseconds}ms");
                // turn off the LED
                controller.Write(ledPin1, PinValue.High);
                Thread.Sleep(dimTimeInMilliseconds);

                // turn on the LED
                controller.Write(ledPin2, PinValue.Low);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"LED1 off for {dimTimeInMilliseconds}ms");
                // turn off the LED
                controller.Write(ledPin2, PinValue.High);
                Thread.Sleep(dimTimeInMilliseconds);

                // turn on the LED
                controller.Write(ledPin3, PinValue.Low);
                Thread.Sleep(lightTimeInMilliseconds);
                Console.WriteLine($"LED1 off for {dimTimeInMilliseconds}ms");
                // turn off the LED
                controller.Write(ledPin3, PinValue.High);
                Thread.Sleep(dimTimeInMilliseconds);
            }



            //Console.ReadLine();
        }
    }
}
