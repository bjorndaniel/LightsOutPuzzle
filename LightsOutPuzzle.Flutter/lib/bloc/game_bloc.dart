import 'dart:async';

import 'package:bloc/bloc.dart';
import 'package:lightsoutpuzzleflutter/model/model.dart';
import 'package:meta/meta.dart';
import 'package:equatable/equatable.dart';
import 'package:rxdart/rxdart.dart';

part 'game_event.dart';
part 'game_state.dart';

class GameBloc extends Bloc<GameEvent, GameState> {
  Game _game;
  BehaviorSubject<Game> gameController = BehaviorSubject<Game>();
  @override
  GameState get initialState => _inital();

  GameState _inital() {
    _game = Game(isFinished: true);
    gameController.sink.add(_game);
    return GameInitialState();
  }

  @override
  Stream<GameState> mapEventToState(
    GameEvent event,
  ) async* {
    if (event is StartGameEvent) {
      _game = Game();
      gameController.sink.add(_game);
      yield GameStartedState();
    }
    if (event is TileTappedEvent) {
      _game.flipTile(event.tile.row, event.tile.column);
      gameController.sink.add(_game);
      if (_game.isCompleted) {
        yield GameCompletedState();
      } else {
        yield TileFlippedState();
      }
    }
    if (event is RestartEvent) {
      _game = Game(isFinished: true);
      gameController.sink.add(_game);
      yield GameInitialState();
    }
  }

  void dispose() => gameController.close();
}
