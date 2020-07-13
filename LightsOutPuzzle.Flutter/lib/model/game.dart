import 'dart:math';

import 'package:tuple/tuple.dart';

import 'model.dart';
import 'package:collection/collection.dart';

class Game {
  List<Tile> _playingField = List<Tile>();
  final _random = Random();
  final eq = const ListEquality().equals;
  final _possibleSolutions = [
    Tuple2([0, 1, 2], [1]),
    Tuple2([0, 1, 3, 4], [2]),
    Tuple2([0, 2, 3], [4]),
    Tuple2([0, 4], [0, 1]),
    Tuple2([1, 2, 4], [0]),
    Tuple2([1, 3], [0, 3]),
    Tuple2([2, 3, 4], [3])
  ];

  Game({isFinished = false}) {
    _createGame(isFinished);
  }

  bool get isCompleted =>
      _playingField.where((_) => !_.imageVisible).length == 0;

  bool get solveable => _solveable(_playingField);

  List<Tile> getHiddenTiles() =>
      _playingField.where((_) => !_.imageVisible).toList();

  List<Tile> flipTile(int row, int column) =>
      _flipTile(row, column, _playingField);

  Tile getTile(int row, int column) => _getTile(row, column, _playingField);

  void _createGame(bool isFinished) {
    var result = _getSolvableGame(isFinished);

    _playingField = result;
  }

  void giveUp() {
    _solveGame(_playingField);
  }

  List<Tile> _getSolvableGame(bool isFinished) {
    var result = _createTiles(isFinished);
    if (isFinished) {
      return result;
    }
    while (!_solveable(result)) {
      result = _createTiles(isFinished);
    }
    return result;
  }

  List<Tile> _createTiles(bool isFinished) {
    var result = List<Tile>();
    for (var row = 0; row < 5; row++) {
      for (var col = 0; col < 5; col++) {
        var tile =
            Tile(row, col, visible: isFinished || _random.nextInt(100) > 30);
        if (!tile.imageVisible) {
          tile.previousState = true;
        }
        result.add(tile);
      }
    }
    return result;
  }

  List<Tile> _flipTile(int row, int column, List<Tile> game) {
    var toFlip = List<Tile>();
    var tile = _getTile(row, column, game);
    toFlip.add(tile);
    //The field above
    if (tile.row - 1 >= 0) {
      toFlip.add(_getTile(tile.row - 1, tile.column, game));
    }
    // The field underneath
    if (tile.row + 1 < 5) {
      toFlip.add(_getTile(tile.row + 1, tile.column, game));
    }
    // The field to the left
    if (tile.column - 1 >= 0) {
      toFlip.add(_getTile(tile.row, tile.column - 1, game));
    }
    // The field to the right
    if (tile.column + 1 < 5) {
      toFlip.add(_getTile(tile.row, tile.column + 1, game));
    }
    toFlip.forEach((_) => _.flip());
    var reset = game.where(
        (t) => !toFlip.any((f) => f.row == t.row && f.column == t.column));
    reset.forEach((_) {
      _.previousState = _.imageVisible;
    });
    return toFlip;
  }

  Tile _getTile(int row, int column, List<Tile> game) =>
      game.firstWhere((_) => _.row == row && _.column == column,
          orElse: () => null);

  void _solveGame(List<Tile> game) {
    for (var i = 0; i < 4; i++) {
      _flipRow(i, game);
    }
    var lastRow = game.where((_) => _.row == 4 && !_.imageVisible);
    var columns = lastRow.map((_) => _.column).toList();
    var solution = _possibleSolutions.firstWhere((s) => eq(s.item1, columns),
        orElse: () => null);
    if (solution == null) {
      throw Exception('Game is not solveable, something is wrong');
    }
    for (var column in solution.item2) {
      _flipTile(0, column, game);
    }
    for (var i = 0; i < 4; i++) {
      _flipRow(i, game);
    }
    if (game.firstWhere((_) => !_.imageVisible, orElse: () => null) != null) {
      print('Solution:');
      for (var c in solution.item2) {
        print('$c');
      }
      throw Exception('Game should be solved but was not!');
    }
  }

  bool _solveable(List<Tile> game) {
    var clone = game.map((_) => _.clone()).toList();
    for (var i = 0; i < 4; i++) {
      _flipRow(i, clone);
    }
    var lastRow = clone.where((_) => _.row == 4 && !_.imageVisible);
    var columns = lastRow.map((_) => _.column).toList();
    return _possibleSolutions.firstWhere((s) => eq(s.item1, columns),
            orElse: () => null) !=
        null;
  }

  void _flipRow(int row, List<Tile> game) {
    if (row > 3) {
      return;
    }
    for (var t in game.where((_) => _.row == row && !_.imageVisible)) {
      _flipTile(t.row + 1, t.column, game);
    }
  }
}
