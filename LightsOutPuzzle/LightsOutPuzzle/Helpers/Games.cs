using System;

namespace LightsOutPuzzle.Helpers
{
    public class Games
    {
        private static Random _random = new Random();

        public static bool[,] RandomGame()
        {
            var gameField = new bool[5, 5];
            for(int row = 0; row < 5; row++)
            {
                for(int col = 0; col< 5; col++)
                {
                    gameField[row, col] = _random.Next(0, 100) > 30;
                }
            }
            return gameField;
        }
    }
}
