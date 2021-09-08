package com.example.lightsoutpuzzlejetpack

import kotlin.random.Random

class Game(isFinished: Boolean) {

    private var posibleSolutions: List<Pair<List<Int>, List<Int>>> = listOf()
    var playingField: List<Tile> = listOf()
    var isStarted: Boolean = false

    init {
        playingField = createTiles(isFinished)
        isStarted = !isFinished
    }

    //TODO: Add function to make sure all games are solveable
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

    fun getTile(row: Int, col: Int): Tile {
        return playingField.filter { it.row == row && it.col == col }.first()
    }

    fun flipTile(row: Int, col: Int, tiles: List<Tile>): List<Tile> {
        val flipped = mutableListOf<Tile>()
        val tile = tiles.filter { it.row == row && it.col == col }.first()
        for (t in tiles) {
            if (t.row == tile.row && t.col == tile.col) {
                flipped.add(copyTile(t))
            } else if (tile.row - 1 >= 0 && t.row == tile.row - 1 && t.col == tile.col) {
                flipped.add(copyTile(t))
            } else if (tile.row + 1 <= 4 && t.row == tile.row + 1 && t.col == tile.col) {
                flipped.add(copyTile(t))
            } else if (tile.col - 1 >= 0 && t.row == tile.row && t.col == tile.col - 1) {
                flipped.add(copyTile(t))
            } else if (tile.col + 1 <= 4 && t.row == tile.row && t.col == tile.col + 1) {
                flipped.add(copyTile(t))
            } else {
                flipped.add(t)
            }
        }
        return flipped
    }

    private fun copyTile(t: Tile): Tile {
        val newTile = Tile(row = t.row, col = t.col, visible = t.imageVisible)
        newTile.flip()
        return newTile
    }

    fun isWon(): Boolean {
        var count = playingField.filter { !it.imageVisible }.count()
        return count == 0
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