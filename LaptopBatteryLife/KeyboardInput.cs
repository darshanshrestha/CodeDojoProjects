using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LaptopBatteryLife
{
    public class KeyboardInput : IKeyboardInput
    {
        public double Read()
        {
            var line = Console.ReadLine();
            return double.Parse(line);
        }
    }
}
