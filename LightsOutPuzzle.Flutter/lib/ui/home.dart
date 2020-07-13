import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:lightsoutpuzzleflutter/bloc/game_bloc.dart';
import 'package:lightsoutpuzzleflutter/model/model.dart';
import 'package:oktoast/oktoast.dart';

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: BlocBuilder<GameBloc, GameState>(builder: (context, state) {
        final bloc = BlocProvider.of<GameBloc>(context);
        if (state is GameCompletedState) {
          Future.delayed(const Duration(milliseconds: 100), () {
            showToast(
              'Well done, the Avengers are proud!',
              duration: Duration(seconds: 2),
              position: ToastPosition.center,
              backgroundColor: Colors.green,
              radius: 5.0,
              textStyle: TextStyle(fontSize: 16.0, color: Colors.white),
            );
          });
        }
        return SafeArea(
          child: Container(
            height: MediaQuery.of(context).size.height,
            width: MediaQuery.of(context).size.width,
            decoration: BoxDecoration(
              gradient: LinearGradient(
                  colors: [Color(0xFF111111), Color(0xFF444444)],
                  begin: Alignment.topCenter,
                  end: Alignment.bottomCenter),
            ),
            child: SingleChildScrollView(
              child: Padding(
                padding: const EdgeInsets.only(
                  top: 30,
                  bottom: 30,
                  left: 10,
                  right: 10,
                ),
                child: Column(
                  children: [
                    Text(
                      'A Flutter game based on \'Lights Out\' with a Marvel-twist.',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                        fontSize: 24,
                        color: Colors.white,
                      ),
                    ),
                    StreamBuilder(
                      stream: bloc.gameController,
                      builder: (context, snapshot) {
                        return GridView.count(
                          padding: const EdgeInsets.all(20),
                          shrinkWrap: true,
                          crossAxisSpacing: 2,
                          mainAxisSpacing: 2,
                          crossAxisCount: 5,
                          children:
                              _createGrid(context, bloc.gameController.value),
                        );
                      },
                    ),
                    Align(
                      alignment: Alignment.topCenter,
                      child: RaisedButton(
                        onPressed: () {
                          BlocProvider.of<GameBloc>(context)
                              .add(StartGameEvent());
                        },
                        color: Colors.blue,
                        textColor: Colors.white,
                        child: Text(
                          state is GameInitialState
                              ? 'Start game'
                              : 'Randomize and start over',
                        ),
                      ),
                    ),
                    Text(
                      'Click on a tile to adjust the state of tile and the directly adjacent ones.',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                        fontSize: 16,
                        color: Colors.white,
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.only(top: 5, bottom: 5),
                      child: Text(
                        'Keep on clicking to reveal the complete Marvel picture.',
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 16,
                          color: Colors.white,
                        ),
                      ),
                    ),
                    Text(
                      'Created by Marcofolio.net based on the electronic game \'Lights Out\' from 1995 by Tiger Electronics. Picture used from Marvels \'Avengers: Infinity War\' 2018 movie.',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                        fontSize: 10,
                        color: Colors.white,
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ),
        );
      }),
    );
  }

  List<Widget> _createGrid(BuildContext context, Game game) {
    var children = List<Widget>();
    for (var row = 0; row < 5; row++) {
      for (var col = 0; col < 5; col++) {
        final tile = game.getTile(row, col);
        children.add(
          GestureDetector(
            onTap: () => game.isCompleted
                ? null
                : BlocProvider.of<GameBloc>(context).add(TileTappedEvent(tile)),
            child: Container(
                height: 50,
                width: 50,
                color: Colors.white,
                child: tile.imageVisible
                    ? Image(
                        image: AssetImage(
                            'assets/images/row-${row + 1}-col-${col + 1}.jpg'),
                      )
                    : Image(
                        image: AssetImage('assets/images/icon.png'),
                      )),
          ),
        );
      }
    }
    return children;
  }
}
