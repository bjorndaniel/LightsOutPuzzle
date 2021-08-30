package com.example.lightsoutpuzzlejetpack

data class Message(val author:String, val body:String)

data class Game(
    var isStarted: Boolean
)