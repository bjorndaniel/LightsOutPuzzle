using System;

namespace LightsOutPuzzle
{
    public class Games
    {
        private static Random _random = new Random();

        public static bool[,] RandomGame()
        {
            var gameField = new bool[5, 5];
            for(int row = 0; row < 5;)
            {
                for(int col = 0; col< 5;)
                {
                    gameField[row, col] = _random.Next(0, 100) > 50;
                }
            }
            return gameField;
        }
    }
}
