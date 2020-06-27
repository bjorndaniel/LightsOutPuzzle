using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOutPuzzle.Common
{
    public class Game
    {
        private static Random _random = new Random();
        private List<Tile> _playingField;

        internal Game()
        {
            CreateGame();
        }

        public Tile GetTile(int row, int column) =>
            _playingField.First(_ => _.Row == row && _.Column == column);

        /// <summary>
        /// Flips the tile and all adjacent tiles (horizontally and vertically)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public IEnumerable<Tile> FlipTile(int row, int column)
        {
            var toFlip = new List<Tile>();
            var tile = GetTile(row, column);
            toFlip.Add(tile);
            //The field above
            if (tile.Row - 1 >= 0)
            {
                toFlip.Add(GetTile(tile.Row - 1, tile.Column));
            }
            // The field underneath
            if (tile.Row + 1 < 5)
            {
                toFlip.Add(GetTile(tile.Row + 1, tile.Column));
            }
            // The field to the left
            if (tile.Column - 1 >= 0)
            {
                toFlip.Add(GetTile(tile.Row, tile.Column - 1));
            }
            // The field to the right
            if (tile.Column + 1 < 5)
            {
                toFlip.Add(GetTile(tile.Row, tile.Column + 1));
            }
            toFlip.ForEach(_ => _.Flip());
            return toFlip;
        }

        public IEnumerable<Tile> GetFlippedTiles() =>
            _playingField.Where(_ => _.ImageVisible);

        public IEnumerable<Tile> HideAll()
        {
            _playingField.ForEach(_ => _.ImageVisible = false);
            return _playingField;
        }

        public IEnumerable<Tile> ShowAll()
        {
            _playingField.ForEach(_ => _.ImageVisible = true);
            return _playingField;
        }

        public bool GameCompleted() =>
            _playingField.All(_ => _.ImageVisible);

        public IEnumerable<Tile> GetAll() =>
            _playingField;

        public static Game RandomGame() => new Game();

        private void CreateGame()
        {
            _playingField = new List<Tile>();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    _playingField.Add(new Tile(row, col, _random.Next(0, 100) > 30));
                }
            }
        }
    }
}