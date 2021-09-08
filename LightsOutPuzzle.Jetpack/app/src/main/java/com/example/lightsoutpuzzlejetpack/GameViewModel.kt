package com.example.lightsoutpuzzlejetpack

import android.util.Log
import androidx.compose.foundation.isSystemInDarkTheme
import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel


class GameViewModel : ViewModel()  {

    private var game: Game = Game(isFinished = true)

    val isStarted: MutableState<Boolean> = mutableStateOf(false)
    val isWon: MutableState<Boolean> = mutableStateOf(false)
    val playingField: MutableState<List<Tile>> = mutableStateOf(listOf())
    init{
        playingField.value = game.playingField
    }

    fun startGame(){
        game = Game(isFinished = false)
        game.isStarted = true
        isStarted.value = true
        isWon.value = false
        playingField.value = game.playingField
    }

    fun flipTile(row:Int, col:Int){
        playingField.value = game.flipTile(row, col, game.playingField)
        game.playingField = playingField.value
        if(game.isWon()){
            isWon.value = true
            isStarted.value = false
        }
    }
}