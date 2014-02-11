using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopBatteryLife
{
    public class BatteryLifePredictor
    {
        private readonly IKeyboardInput _keyboardInput;
        //public double _chargingFactor;
        //private decimal _totalChargeTime;
        private decimal _chargeCeiling;
        private decimal _batteryLifeCeiling;

        public BatteryLifePredictor(IKeyboardInput keyboardInput)
        {
            _keyboardInput = keyboardInput;

            //_totalChargeTime = 0;
            _chargeCeiling = _batteryLifeCeiling = 0;
        }


        public decimal Predict()
        {
            InputValue = _keyboardInput.Read();

            return _chargeCeiling <= 0
                       ? 0
                       : Math.Min((decimal) InputValue*_batteryLifeCeiling/_chargeCeiling, _batteryLifeCeiling);
        }

        public double InputValue { get; private set; }

        public void Calculate(decimal chargingTime, decimal batteryLife)
        {
            //_chargingFactor = _calculator.ProcessChargeTimes();

            //_totalChargeTime += chargingTime;
            
            if (_batteryLifeCeiling < batteryLife)
            {
                _batteryLifeCeiling = batteryLife;
                if (_chargeCeiling < chargingTime)
                {
                    _chargeCeiling = chargingTime;
                }
            }
            else if (_batteryLifeCeiling == batteryLife)
            {
                if (_chargeCeiling > chargingTime)
                {
                    _chargeCeiling = chargingTime;
                }
            }

           

        }
    }
}
