package com.example.lightsoutpuzzlejetpack

import android.content.res.Configuration
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.animateColorAsState
import androidx.compose.animation.animateContentSize
import androidx.compose.foundation.*
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.lightsoutpuzzlejetpack.R.drawable.*
import com.example.lightsoutpuzzlejetpack.ui.theme.LightsOutPuzzleJetpackTheme
import androidx.compose.foundation.lazy.items
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.fontResource
import androidx.compose.ui.text.AnnotatedString
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.sp

class MainActivity : ComponentActivity() {
    val DarkColors = darkColors(
        primary = Color(red= 1f, green = 1f, blue = 1f),
    )
    val LightColors = lightColors(
        primary = Color(red = 0f, green = 0f, blue = 0f)
    )
    @ExperimentalFoundationApi
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            MaterialTheme(colors = if (isSystemInDarkTheme()) DarkColors else LightColors) {
                MainPage()
            }
        }
    }
}

@ExperimentalFoundationApi
@Composable
fun MainPage(){
    val game = Game(isStarted = false)
    var buttonText by remember {mutableStateOf("Start game")}
    Box(modifier = Modifier
        .padding(top = 50.dp, start = 10.dp, end = 10.dp, bottom = 0.dp)
        .fillMaxSize()) {
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Lights out",
                fontSize = 36.sp,
                color = MaterialTheme.colors.primary,
            )
            Spacer(modifier = Modifier.height(18.dp))
            Text("A Jetpack Compose game based on 'Lights out with a Marvel-twist",
            fontSize = 14.sp,
            textAlign = TextAlign.Center,
            color = MaterialTheme.colors.primary)
            Spacer(modifier = Modifier.height(14.dp))
            Box(
                modifier = Modifier
                    .height(250.dp)
                    .fillMaxWidth()
                    .background(Color(red = 0f, green = 1f, blue = 0f)),

            ){
                Column(Modifier.background(Color(red=0f,green = 1f,blue = 0f))) {

                }
            }
            Spacer(modifier = Modifier.height(8.dp))
            Column(){
                Button(onClick = {
                    game.isStarted = !game.isStarted
                    buttonText = if(game.isStarted)  "Restart game" else "Start game"
                },
                colors = ButtonDefaults.textButtonColors(
                    backgroundColor = Color(red=0f, green = 0f, blue= 1f),
                    )) {
                    Text(text = buttonText, color = Color(red=1f,green=1f,blue=1f))
                }
            }
            Spacer(modifier = Modifier.height(14.dp))
            Text("Click on a tile to adjust the state of tile and the directly adjacent ones.",
                fontSize = 14.sp,
                textAlign = TextAlign.Center,
                color = MaterialTheme.colors.primary)
            Text("Keep on clicking to reveal the complete Marvel picture.",
                fontSize = 14.sp,
                textAlign = TextAlign.Center,
                color = MaterialTheme.colors.primary)
            Text("Created by Marcofolio.net based on the electronic game 'Lights Out' from 1995 by Tiger Electronics. Picture used from Marvels 'Avengers: Infinity War' 2018 movie.",
                fontSize = 8.sp,
                textAlign = TextAlign.Center,
                color = MaterialTheme.colors.primary)
        }
    }
}


@ExperimentalFoundationApi
@Preview(name = "Light mode", showBackground = true)
@Preview(uiMode = Configuration.UI_MODE_NIGHT_YES,
showBackground = true,
name = "Dark Mode")
@Composable
fun PreviewMain(){
    val DarkColors = darkColors(
        primary = Color(red= 1f, green = 1f, blue = 1f),
    )
    val LightColors = lightColors(
        primary = Color(red = 0f, green = 0f, blue = 0f)
    )
    MaterialTheme(colors = if (isSystemInDarkTheme()) DarkColors else LightColors) {
        MainPage()
    }
}