part of 'game_bloc.dart';

@immutable
abstract class GameState extends Equatable {
  const GameState();
  @override
  List<Object> get props => [];
}

class GameInitialState extends GameState {}

class GameStartedState extends GameState {
  // final Game game;
  // const GameStartedState(this.game);
  // @override
  // List<Object> get props => [game];
}
