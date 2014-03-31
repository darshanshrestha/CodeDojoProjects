using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BotCleanLarge
{
    public interface IBot
    {
        char[,] Matrix { get; set; }
        int NumberOfMoves { get; }
        Point BotPosition { get; set; }
        string NextMove();
    }

    class Bot : IBot
    {
        public char[,] Matrix { get; set; }
        public int NumberOfMoves { get; private set; }
        public Point BotPosition { get; set; }

        const string Right = "RIGHT";
        const string Left = "LEFT";
        private const string Up = "UP";
        private const string Down = "DOWN";
        private const string Clean = "CLEAN";
        private List<Point> _dirtPositions = new List<Point>();

        
        public Bot(char[,] matrix)
        {
            Matrix = matrix;
            ScanMatrix();
        }

        private void ScanMatrix()
        {
            for (int x = 0; x < Matrix.GetLength(0); x++)
            {
                for (int y = 0; y < Matrix.GetLength(1); y++)
                {
                    var character = Matrix[x, y];

                    if (character == 'b')
                    {
                        BotPosition = new Point(x, y);
                    }
                    else if (character == 'd')
                    {
                        _dirtPositions.Add(new Point(x,y));
                    }
                }
            }
        }

        
        public string NextMove()
        {
            if (Matrix[BotPosition.X, BotPosition.Y] == 'd')
            {
                Matrix[BotPosition.X, BotPosition.Y] = '-';
                _dirtPositions.Remove(new Point(BotPosition.X, BotPosition.Y));
                NumberOfMoves++;
                return Clean;
            }

            int shortestDistance = int.MaxValue;
            Point nextDirtyPoint = new Point();

            

            foreach (var dirtPosition in _dirtPositions)
            {
                int dist = Math.Abs(BotPosition.X - dirtPosition.X) + Math.Abs(BotPosition.Y - dirtPosition.Y);
                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nextDirtyPoint = dirtPosition;
                }
            }

            string movement = null;
            if (BotPosition.X != nextDirtyPoint.X)
            {
                movement = BotPosition.X < nextDirtyPoint.X ?  Down : Up;
            }
            else
            {
                movement = BotPosition.Y < nextDirtyPoint.Y ? Right : Left;
            }

            switch (movement)
            {
                case Down:
                    BotPosition = new Point(BotPosition.X + 1, BotPosition.Y);
                    break;
                case Up:
                    BotPosition = new Point(BotPosition.X - 1, BotPosition.Y);
                    break;
                case Right:
                    BotPosition = new Point(BotPosition.X, BotPosition.Y + 1);
                    break;
                case Left:
                    BotPosition = new Point(BotPosition.X, BotPosition.Y - 1);
                    break;


            }

            NumberOfMoves++;

            return movement;
        }


    }

    
}