part of 'game_bloc.dart';

@immutable
abstract class GameEvent extends Equatable {
  const GameEvent();
  @override
  List<Object> get props => [];
}

class StartGameEvent extends GameEvent {}

class TileTappedEvent extends GameEvent {
  final Tile tile;
  const TileTappedEvent(this.tile);
  List<Object> get props => [tile];
}

class RestartEvent extends GameEvent {
  const RestartEvent();
}
