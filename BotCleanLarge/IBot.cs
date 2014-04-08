using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BotCleanLarge
{
    public interface IBot
    {
        string[] MatrixState { get; set; }
        Position CurrentBotPosition { get; set; }
        string next_move(int botRow, int botColumn, int gridHeight, int gridWidth, string[] grid);
    }

    class Bot : IBot
    {
        const string Right = "RIGHT";
        const string Left = "LEFT";
        private const string Up = "UP";
        private const string Down = "DOWN";
        private const string Clean = "CLEAN";

        public string[] MatrixState { get; set; }

        public Position CurrentBotPosition { get; set; }

        public Bot()
        {
            
        }       

        public string next_move(int botRow, int botColumn, int gridHeight, int gridWidth, string[] grid)
        {
            var matrix = ConvertToDoubleArray(gridHeight, gridWidth, grid);

            var botPosition = new Position(botRow, botColumn);

            var dirtPositions = ScanMatrix(matrix);


            if (matrix[botPosition.Row, botPosition.Column] == 'd')
            {
                matrix[botPosition.Row, botPosition.Column] = '-';
                dirtPositions.Remove(new Position(botPosition.Row, botPosition.Column));
                CaptureState(gridHeight, matrix);
                return Clean;
            }

            var shortestDistance = int.MaxValue;

            var nextDirtyPoint = new Position();

            foreach (var dirtPosition in dirtPositions)
            {
                int dist = Math.Abs(botPosition.Row - dirtPosition.Row) + Math.Abs(botPosition.Column - dirtPosition.Column);
                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nextDirtyPoint = dirtPosition;
                }
            }

            string movement = null;
            if (botPosition.Row != nextDirtyPoint.Row)
            {
                movement = botPosition.Row < nextDirtyPoint.Row ? Down : Up;
            }
            else
            {
                movement = botPosition.Column < nextDirtyPoint.Column ? Right : Left;
            }

            matrix[botPosition.Row, botPosition.Column] = '-';

            switch (movement)
            {
                case Down:
                    CurrentBotPosition = new Position(botPosition.Row + 1, botPosition.Column);
                    break;
                case Up:
                    CurrentBotPosition = new Position(botPosition.Row - 1, botPosition.Column);
                    break;
                case Right:
                    CurrentBotPosition = new Position(botPosition.Row, botPosition.Column + 1);
                    break;
                case Left:
                    CurrentBotPosition = new Position(botPosition.Row, botPosition.Column - 1);
                    break;
            }

            if (matrix[CurrentBotPosition.Row, CurrentBotPosition.Column] != 'd')
                matrix[CurrentBotPosition.Row, CurrentBotPosition.Column] = 'b';


            CaptureState(gridHeight, matrix);


            return movement;
        }

        private char[,] ConvertToDoubleArray(int gridHeight, int gridWidth, string[] grid)
        {
            var matrix = new char[gridHeight,gridWidth];

            for (var rowIndex = 0; rowIndex < grid.Length; rowIndex++)
            {
                var row = grid[rowIndex];

                for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    var character = row[columnIndex];
                    matrix[rowIndex, columnIndex] = character;
                }
            }
            return matrix;
        }

        private static List<Position> ScanMatrix(char[,] matrix)
        {
            var dirtPositions = new List<Position>();

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    var character = matrix[x, y];

                    if (character == 'd')
                    {
                        dirtPositions.Add(new Position(x, y));
                    }
                }
            }
            return dirtPositions;
        }

        private void CaptureState(int gridHeight, char[,] matrix)
        {
            MatrixState = new string[gridHeight];

            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                var builder = new StringBuilder();

                for (int columnIndex = 0; columnIndex < matrix.GetLength(1); columnIndex++)
                {
                    var character = matrix[rowIndex, columnIndex];

                    builder.Append(character);
                }

                MatrixState[rowIndex] = builder.ToString();
            }
        }
    }
}