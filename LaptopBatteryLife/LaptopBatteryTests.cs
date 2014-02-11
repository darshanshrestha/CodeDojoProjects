using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using System.IO;

namespace LaptopBatteryLife
{
    [TestFixture]
    class given_user_input
    {

        [Test]
        public void when_the_user_enters_a_value_for_charge_time_then_the_value_should_be_accessible()
        {
            //Arrange
            //TODO: Mock the UI call for getting the value
            double batLife = 2.5;
            var mockKeyboardInput = NSubstitute.Substitute.For<IKeyboardInput>();
            mockKeyboardInput.Read().Returns(batLife);
                        
            //Act
            var mockedValue = mockKeyboardInput.Read();
            //Assert
            Assert.AreEqual(mockedValue, batLife);
        }

        [Test]
        public void when_i_enter_charge_hours_then_the_the_output_should_be_the_hours_multiplied_by_the_factor()
        {
            //Arrange
            double batLife = 2.5;
            double chargeTime = 1.5;
            var mockKeyboardInput = NSubstitute.Substitute.For<IKeyboardInput>();
            mockKeyboardInput.Read().Returns(chargeTime);
            BatteryLifePredictor predictor = new BatteryLifePredictor(mockKeyboardInput);
            
            //calc.ProcessChargeTimes().Returns(chargeTime);
            
            //Act
            predictor.Calculate(1,2);
            predictor.Calculate(5, 4);
            predictor.Calculate(2,4);

            //Assert
            Assert.AreEqual(predictor.Predict(), 3);
        }

        [Test]
        public void when_i_enter_charge_hours_greater_than_the_chargeceiling_the_output_should_be_the_batterylifeceiling()
        {
            //Arrange
            double chargeTime = 10;
            var mockKeyboardInput = NSubstitute.Substitute.For<IKeyboardInput>();
            mockKeyboardInput.Read().Returns(chargeTime);
            BatteryLifePredictor predictor = new BatteryLifePredictor(mockKeyboardInput);

            //calc.ProcessChargeTimes().Returns(chargeTime);

            //Act
            predictor.Calculate(1, 2);
            predictor.Calculate(5, 4);
            predictor.Calculate(2, 4);

            //Assert
            Assert.AreEqual(predictor.Predict(), 4);

        }


    }
}
