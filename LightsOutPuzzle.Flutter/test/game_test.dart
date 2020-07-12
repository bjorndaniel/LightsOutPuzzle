import 'package:flutter_test/flutter_test.dart';
import 'package:lightsoutpuzzleflutter/model/model.dart';

void main() {
  test('Can flip tile', () {
    final tile = Tile(1, 1);
    expect(tile.imageVisible, false);
    tile.flip();
    expect(tile.imageVisible, true);
    expect(tile.hasBeenFlipped, true);
  });

  test('Can create finished game', () {
    final game = Game(isFinished: true);
    expect(game.isCompleted, true);
  });

  test('Can create random game', () {
    final game = Game();
    expect(game.isCompleted, false);
    expect(game.getHiddenTiles().length > 0, true);
    expect(game.solveable, true);
    game.giveUp();
    expect(game.isCompleted, true);
  });
  test('Can solve game', () {
    final game = Game();
    expect(game.isCompleted, false);
    game.giveUp();
    expect(game.isCompleted, true);
  });
}
