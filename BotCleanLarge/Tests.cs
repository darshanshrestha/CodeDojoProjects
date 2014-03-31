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
            var matrix = new char[5,5]
                {
                    {'b', 'd', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'}
                };
            bot = new Bot(matrix);
           
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
            var matrix = new char[5, 5]
                {
                    {'d', 'b', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'}
                };
            bot = new Bot(matrix);


            //Act
            string move = bot.NextMove();

            //Assert
            move.Should().Be("LEFT");
            AssertMatrixIsClean();
        }

        [Test]
        public void when_bot_lands_on_dirty_then_next_move_is_clean()
        {
            //Arrange
            //Arrange
            var matrix = new char[5, 5]
                {
                    {'d', 'b', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'}
                };
            bot = new Bot(matrix);
            bot.NextMove();
            string move = bot.NextMove();

            //Assert
            move.Should().Be("CLEAN");
            AssertMatrixIsClean();
        }

        [Test]
        public void test_case_3()
        {
            bot.Matrix = new char[5, 5]
                {
                    {'b', '-', '-', '-', 'd'},
                    {'d', '-', '-', '-', 'd'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-'}
                };

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
            //Arrange
            bot.Matrix = new char[5, 5]
                {
                    {'-', '-', '-', '-', 'd'},
                    {'-', '-', '-', '-', '-'},
                    {'-', '-', '-', 'd', '-'},
                    {'d', '-', 'd', '-', 'b'},
                    {'-', '-', '-', '-', '-'}
                };

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
            //foreach (var row in bot.Matrix)
            //{
            //    foreach (var character in row)
            //    {
            //        character.Should().Be('-');
            //    }
            //}
        }





    }
}
