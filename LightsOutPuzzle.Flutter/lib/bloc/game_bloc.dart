import 'dart:async';

import 'package:bloc/bloc.dart';
import 'package:flutter/rendering.dart';
import 'package:meta/meta.dart';
import 'package:equatable/equatable.dart';
import 'package:rxdart/rxdart.dart';

part 'game_event.dart';
part 'game_state.dart';

class GameBloc extends Bloc<GameEvent, GameState> {
  // Game _game;

  @override
  GameState get initialState => GameInitialState();
  @override
  Stream<GameState> mapEventToState(
    GameEvent event,
  ) async* {
    // TODO: implement mapEventToState
  }
}
