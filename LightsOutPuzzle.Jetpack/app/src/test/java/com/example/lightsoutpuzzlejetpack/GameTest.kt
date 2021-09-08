package com.example.lightsoutpuzzlejetpack

import org.junit.Test
import org.junit.Assert.*

class GameTest {

    @Test
    fun canGetTile() {
        val game = Game(isFinished = false)
        val tile = game.getTile(2,3)
        assertEquals(2, tile.row)
        assertEquals(3, tile.col)
    }
}