using System;
using System.Collections.Generic;
using System.Linq;

namespace LaptopBatteryLife
{
    public interface ICalculator
    {
        double ProcessChargeTimes();
    }

    class Calculator : ICalculator
    {
        private List<double[]> ReadHistoryFile()
        {
            var fileMetrics = new List<double[]>();
            var filePath = "TrainingData.txt";
            var lines = System.IO.File.ReadLines(filePath);
            foreach (var line in lines)
            {
                var splitValues = line.Split(',');
                var chgTime = double.Parse(splitValues[0]);
                var batteryLifeTime = double.Parse(splitValues[1]);
                fileMetrics.Add(new double[]{chgTime,batteryLifeTime});
            }

            return fileMetrics;
        }

        public double ProcessChargeTimes()
        {
            //Read from file
            //var metrics = this.ReadHistoryFile();

            //calculate
            //double maxBatteryLifeTime = (from x in metrics select x[1]).Max();
            //double averageBatteryLifeTime =(from )

            return 0;
        }
    }
}