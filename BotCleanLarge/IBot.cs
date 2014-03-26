using System;

namespace BotCleanLarge
{
    public interface IBot
    {
        string[] Matrix { get; set; }
        int NumberOfMoves { get; }
        Tuple<int, int> BotPosition { get; set; }
        string NextMove();
    }
}