using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace BotCleanLarge
{
    [TestFixture]
    public class Tests
    {
        private IBot bot;

        [SetUp]
        public void InitPerTest()
        {
            //Cerate an instance of your bot here
        }

        [Test]
        public void test_case_1()
        {
            //Arrange

            bot.Matrix = new string[] 
            {"bd---", 
             "-----", 
             "-----", 
             "-----", 
             "-----"};
            

            //Act
            string move = bot.NextMove();
            
            //Assert
            move.Should().Be("RIGHT");
            AssertMatrixIsClean();
        }

        [Test]
        public void test_case_2()
        {
            //Arrange

            bot.Matrix = new string[] 
            {"db---", 
             "-----", 
             "-----", 
             "-----", 
             "-----"};


            //Act
            string move = bot.NextMove();

            //Assert
            move.Should().Be("LEFT");
            AssertMatrixIsClean();
        }

        [Test]
        public void test_case_3()
        {
            bot.Matrix = new string[]
                {"b---d", 
                 "d---d", 
                 "-----", 
                 "-----", 
                 "-----"};

            //Act

            for (int move = 0; move < 12; move++)
            {
                bot.NextMove();
            }

            bot.NumberOfMoves.Should().Be(12);
            AssertMatrixIsClean();

        }

        [Test]
        public void test_case_4()
        {
            bot.Matrix = new string[]
                {"----d", 
                 "-----", 
                 "---d-", 
                 "d-d-b", 
                 "-----"};

            //Act

            for (int move = 0; move < 14; move++)
            {
                bot.NextMove();
            }

            bot.NumberOfMoves.Should().Be(14);
            AssertMatrixIsClean();

        }


        private void AssertMatrixIsClean()
        {
            foreach (var row in bot.Matrix)
            {
                foreach (var character in row)
                {
                    character.Should().Be('-');
                }
            }
        }





    }
}
