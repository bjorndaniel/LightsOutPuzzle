import 'package:flutter/material.dart';

class Home extends StatelessWidget {
  const Home({Key key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
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
                  GridView.count(
                    padding: const EdgeInsets.all(20),
                    shrinkWrap: true,
                    crossAxisSpacing: 2,
                    mainAxisSpacing: 2,
                    crossAxisCount: 5,
                    children: _createGrid(),
                  ),
                  Align(
                    alignment: Alignment.topCenter,
                    child: RaisedButton(
                      onPressed: () {},
                      color: Colors.blue,
                      textColor: Colors.white,
                      child: Text(
                        'Start game',
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
      ),
    );
  }

  List<Widget> _createGrid() {
    var children = List<Widget>();
    for (var row = 1; row < 6; row++) {
      for (var col = 1; col < 6; col++) {
        children.add(
          Container(
            height: 50,
            width: 50,
            color: Colors.red,
            child: Image(
              image: AssetImage('assets/images/row-$row-col-$col.jpg'),
            ),
          ),
        );
      }
    }
    return children;
  }
}
