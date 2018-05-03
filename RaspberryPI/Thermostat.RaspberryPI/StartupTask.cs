using System;
using Windows.ApplicationModel.Background;
using System.Threading.Tasks;

// Add using statements to the GrovePi libraries
using GrovePi;
using GrovePi.Sensors;
using GrovePi.I2CDevices;

namespace Thermostat
{
    public sealed class StartupTask : IBackgroundTask
    {
        const int MinDesiredTemperature = 15;
        const int MaxDesiredTemperature = 30;

        ILed redLed;
        ILed greenLed;
        IRotaryAngleSensor angleSensor;
        ITemperatureSensor temperatureSensor;
        IOLEDDisplay9696 oled;


        public void Run(IBackgroundTaskInstance taskInstance)
        {
            redLed = DeviceFactory.Build.Led(Pin.DigitalPin2);
            greenLed = DeviceFactory.Build.Led(Pin.DigitalPin3);
            angleSensor = DeviceFactory.Build.RotaryAngleSensor(Pin.AnalogPin2);
            temperatureSensor = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin1);

            oled = DeviceFactory.Build.OLEDDisplay9696();
            oled.initialize();
            oled.clearDisplay();
            oled.setNormalDisplay();
            oled.setVerticalMode();
            oled.setGrayLevel(15);

            oled.setTextXY(2, 0);
            oled.putString($"Temperatuur:");
            oled.setTextXY(4, 0);
            oled.putString($"Huidige:");
            oled.setTextXY(7, 0);
            oled.putString($"Gewenst:");

            // Loop endlessly
            while (true)
            {
                Task.Delay(100).Wait();
                try
                {
                    var angleValue = angleSensor.SensorValue();
                    var desiredTemperature = MinDesiredTemperature + ((MaxDesiredTemperature - MinDesiredTemperature) * angleValue / 1024);
                    var currentTemperature = temperatureSensor.TemperatureInCelsius();

                    oled.setTextXY(5, 0);
                    oled.putString($"{currentTemperature:##.0}        ");
                    oled.setTextXY(8, 0);
                    oled.putString($"{desiredTemperature}");

                    System.Diagnostics.Debug.WriteLine("temperature is :" + currentTemperature + ", desired is: " + desiredTemperature);
                    if (currentTemperature < desiredTemperature)
                    {
                        redLed.ChangeState(SensorStatus.On);
                        greenLed.ChangeState(SensorStatus.Off);
                    }
                    else
                    {
                        redLed.ChangeState(SensorStatus.Off);
                        greenLed.ChangeState(SensorStatus.On);
                    }

                }
                catch (Exception ex)
                {
                    // NOTE: There are frequent exceptions of the following:
                    // WinRT information: Unexpected number of bytes was transferred. Expected: '. Actual: '.
                    // This appears to be caused by the rapid frequency of writes to the GPIO
                    // These are being swallowed here/

                    // If you want to see the exceptions uncomment the following:
                    // System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
