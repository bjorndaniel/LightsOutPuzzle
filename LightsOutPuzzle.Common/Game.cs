﻿namespace LightsOutPuzzle.Common;

public class Game
{
    private static readonly Random _random = new ();
    private List<Tile> _playingField;

    internal Game(bool finished = false)
    {
        CreateGame(finished);
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

    public IEnumerable<Tile> GetHiddenTiles() =>
        _playingField.Where(_ => !_.ImageVisible);

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

    public static Game RandomGame() => new ();

    public static Game FinishedGame() => new (true);

    public Game UpdateGame(IEnumerable<Tile> tilesToUpdate)
    {
        foreach (var t in tilesToUpdate)
        {
            var replace = _playingField.First(_ => _.Row == t.Row && _.Column == t.Column);
            _playingField.Remove(replace);
            _playingField.Add(t);
        }
        return this;
    }

    private void CreateGame(bool finished = false)
    {
        _playingField = [];
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                _playingField.Add(new Tile(row, col, finished || _random.Next(0, 100) > 30));
            }
        }
    }
}