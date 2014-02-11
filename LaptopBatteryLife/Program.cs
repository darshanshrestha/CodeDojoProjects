using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopBatteryLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var predictor = new BatteryLifePredictor(new KeyboardInput());

            var lines = File.ReadAllLines(@"trainingdata.txt");
            foreach (var line in lines)
            {
                var valueStrings = line.Split(',');
                predictor.Calculate(decimal.Parse(valueStrings[0]),  decimal.Parse(valueStrings[1]));
            }

            string command = string.Empty;
            while (predictor.InputValue >= 0)
            {
                Console.WriteLine(String.Format("Expected Battery Life: {0}", predictor.Predict())); ;
            }
        }
    }
}
