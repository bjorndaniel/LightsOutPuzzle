part of 'game_bloc.dart';

@immutable
abstract class GameState extends Equatable {
  const GameState();
  @override
  List<Object> get props => [];
}

class GameInitialState extends GameState {
  const GameInitialState() : super();
}

class GameStartedState extends GameState {
  const GameStartedState();
}

class GameCompletedState extends GameState {
  const GameCompletedState();
}

class TileFlippedState extends GameState {
  const TileFlippedState();
}
