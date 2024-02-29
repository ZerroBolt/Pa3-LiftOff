using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ArduinoInput
{
    static string line;
    static SerialPort port;
    public ArduinoInput()
    {
        port = new SerialPort();
        port.PortName = "COM6";
        port.BaudRate = 11920;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();

        //while (true)
        //{
        //    line = port.ReadLine(); // read separated values
        //    //string line = port.ReadExisting(); // when using characters
        //    if (line != "")
        //    {
        //        // OUTPUT: <int> key activated 0-1 ; <int> shift position 0-100 ; <int> steer rotation 0-280
        //        Console.WriteLine("Read from port: " + line);
        //    }

        //    //if (Console.KeyAvailable)
        //    //{
        //    //    ConsoleKeyInfo key = Console.ReadKey();
        //    //    port.Write(key.KeyChar.ToString());  // writing a string to Arduino
        //    //}
        //}

    }

    public static void SubscribeToStepEvent()
    {
        Game.main.OnBeforeStep += ReadInput;

        //UnsubscribeStepEvent();
    }
    public static void UnsubscribeStepEvent()
    {
        Game.main.OnBeforeStep -= ReadInput;
    }

    public static void ReadInput()
    {
        line = port.ReadLine();
        if (line != "")
        {
            // OUTPUT: <int> key activated 0-1 ; <int> shift position 0-100 ; <int> steer rotation 0-280
            Console.WriteLine("Read from port: " + line);
        }
    }

    public static bool GetControllerKey()
    {
        int keyValue = int.Parse(line.Split(';').First());
        Console.WriteLine("Value key: " + keyValue);
        if (keyValue == 0) return false;
        else return true;
    }

    public static int GetShiftPosition()
    {
        int shiftPosition = int.Parse(line.Split(';').GetValue(1).ToString());
        Console.WriteLine("Shift position: " + shiftPosition);
        return shiftPosition;
    }

    public static int GetSteeringRotation()
    {
        int steerRotation = int.Parse(line.Split(';').Last());
        Console.WriteLine("Steer rotation: " + steerRotation);
        return steerRotation;
    }
}
