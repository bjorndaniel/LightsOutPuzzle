package com.example.lightsoutpuzzlejetpack

import kotlin.random.Random

data class Message(val author: String, val body: String) {
    fun bob(): String {
        return author
    }
}

class Game(isFinished: Boolean) {

    private var posibleSolutions: List<Pair<List<Int>, List<Int>>> = listOf()
    private var playingField: List<Tile> = listOf()

    var isStarted: Boolean = false

    init {
        posibleSolutions =
            listOf(
                Pair(listOf(0, 1, 2), listOf(1)),
                Pair(listOf(0, 1, 3, 4), listOf(2)),
                Pair(listOf(0, 2, 3), listOf(4)),
                Pair(listOf(0, 4), listOf(0, 1)),
                Pair(listOf(1, 2, 4), listOf(0)),
                Pair(listOf(1, 3), listOf(0, 3)),
                Pair(listOf(2, 3, 4), listOf(3)),
            )

        createGame(isFinished)
        isStarted = !isFinished
    }


    private fun createGame(isFinished: Boolean) {
        getSolvableGame(isFinished)
    }

    private fun getSolvableGame(isFinished: Boolean) {
        var result = createTiles(isFinished)
       // while (!isSolveable(result)) {
         //   result = createTiles(isFinished)
        //}
        playingField = result
    }

    private fun createTiles(isFinished: Boolean): List<Tile> {
        var result = mutableListOf<Tile>()
        var random = Random.nextInt(0, 100)
        for (row in 0..4) {
            for (col in 0..4) {
                var tile = Tile(row, col, visible = isFinished || random > 30)
                if (!tile.imageVisible) {
                    tile.previousState = true
                }
                result.add(tile)
                random = Random.nextInt(0, 100)
            }
        }
        return result
    }

    private fun isSolveable(tiles: List<Tile>): Boolean {
        var list = tiles.toMutableList()
        for (i in 0..3) {
            flipRow(i, list)
        }
        var lastRow = list.filter { it.row == 4 && !it.imageVisible }
        var columns = lastRow.map { it.col }
        var find =
            posibleSolutions.filter { it.first.containsAll(columns) && columns.containsAll(it.first) }
        return find != null && find.count() > 0
    }

    private fun flipRow(row: Int, tiles: List<Tile>) {
        if (row > 3) {
            return
        }
        for (tile in tiles.filter { it.row == row && !it.imageVisible }) {
            tile.flip()
        }
    }

    fun startGame(){
        createGame(false)
    }

    fun solveable(): Boolean {
        return isSolveable(playingField)
    }

    fun flipTile(row: Int, col: Int): List<Tile> {
        var toFlip = mutableListOf<Tile>()
        var tile = playingField.filter { it.row == row && it.col == col }.first()
        toFlip.add(tile)
        //The field above
        if (tile.row - 1 >= 0) {
            toFlip.add(playingField.filter { it.row == row - 1 && it.col == col }.first())
        }
        // The field underneath
        if (tile.row + 1 < 4) {
            toFlip.add(playingField.filter { it.row == row + 1 && it.col == col }.first())
        }
        // The field to the left
        if (tile.col - 1 >= 0) {
            toFlip.add(playingField.filter { it.row == row && it.col == col - 1 }.first())
        }
        // The field to the right
        if (tile.col + 1 < 4) {
            toFlip.add(playingField.filter { it.row == row && it.col == col + 1 }.first())
        }
        toFlip.map { tile -> tile.flip() }
        for(tile in playingField){
            var reset = toFlip.filter { it.row == tile.row && it.col == tile.col }.count() == 0
            if(reset){
                tile.previousState = tile.imageVisible
            }
        }
        return toFlip;
    }

    fun isFlipped(row:Int, col:Int): Boolean{
        var tile = playingField.filter { it.row == row && it.col == col }.first();
        return !tile.imageVisible
    }

    fun getTile(row: Int, col: Int): Tile{
        return playingField.filter { it.row == row && it.col == col }.first()
    }
}


class Tile(val row: Int, val col: Int, val visible: Boolean) {
    var previousState: Boolean = false
    var imageVisible: Boolean = false

    init {
        imageVisible = visible
    }

    fun flip() {
        previousState = imageVisible
        imageVisible = !imageVisible
    }

    fun hasBeenFlipped(): Boolean {
        return previousState != imageVisible
    }
}