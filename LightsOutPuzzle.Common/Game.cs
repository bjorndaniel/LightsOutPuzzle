using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public bool GameCompleted() =>
            _playingField.All(_ => _.ImageVisible);

        public IEnumerable<Tile> GetAll() =>
            _playingField;

        public static Game RandomGame()
        {
            var g = new Game();
            while (!g.CanBeSolved())
            {
                g = new Game();
            }
            return g;
        }

        //Solution adapted from https://stackoverflow.com/questions/7212966/any-algorithm-for-flip-all-light-out-game
        private bool CanBeSolved()
        {
            //var n = 5;
            //var m = 5;
            //var eqs = new List<>();
            //foreach (var i in Enumerable.Range(0,n))
            //{
            //    foreach (var j in Enumerable.Range(0, m))
            //    {
            //        var eq = new HashSet<object>();
            //        foreach (var d in Enumerable.Range(-1, 2))
            //        {
            //            if (0 <= i + d && i + d < n)
            //            {
            //                eq.Add((i + d) * m + j);
            //            }
            //            if (d != 0 && 0 <= j + d && j + d < m)
            //            {
            //                eq.Add(i * m + j + d);
            //            }
            //        }
            //        eqs.Add(new List<object> {
            //        GetTile(i, j),
            //        eq
            //    });
            //    }
            //}
            //var N = eqs.Count;
            //foreach (var i in Enumerable.Range(0, N))
            //{
            //    foreach (var j in Enumerable.Range(i, N))
            //    {
            //        if (eqs[j][1].Contains(i))
            //        {
            //            eqs[i] = eqs[j];
            //            eqs[j] = eqs[i];
            //            break;
            //        }
            //        else
            //        {
            //            Debug.WriteLine("Unsolvable");
            //            return false;
            //        }

            //    }
            //    foreach (var j in Enumerable.Range(i + 1, N))
            //    {
            //        if (eqs[j][1].Contains(i))
            //        {
            //            eqs[j][0] ^= eqs[i][0];
            //            eqs[j][1] ^= eqs[i][1];
            //        }
            //    }
            //}
            return true;
        }

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
