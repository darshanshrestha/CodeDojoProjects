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
        public int Row { get; set; }
        public int Column { get; set; }
    }
}