namespace BotCleanLarge
{
    public class Position
    {
        public Position()
        {
            
        }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
        //Based on Grid
        public int Row { get; set; }
        public int Column { get; set; }

        //For our Algorithm based on bot postion
        public int X { get; set; }
        public int Y { get; set; }
    }
}