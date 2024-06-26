package com.example.lightsoutpuzzlejetpack

import android.annotation.SuppressLint
import android.content.res.Configuration
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.*
import androidx.compose.animation.core.animateFloatAsState
import androidx.compose.animation.core.tween
import androidx.compose.foundation.*
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.items
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.rotate
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.ImageBitmap
import androidx.compose.ui.graphics.graphicsLayer
import androidx.compose.ui.res.imageResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.ViewModel
import com.example.lightsoutpuzzlejetpack.R.drawable.*
import kotlinx.coroutines.launch
import java.util.*
import androidx.compose.foundation.ExperimentalFoundationApi as ExperimentalFoundationApi1

class MainActivity : ComponentActivity() {
    val DarkColors = darkColors(
        primary = Color(red = 1f, green = 1f, blue = 1f),
    )
    val LightColors = lightColors(
        primary = Color(red = 0f, green = 0f, blue = 0f)
    )

    @ExperimentalAnimationApi
    @ExperimentalFoundationApi1
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            MaterialTheme(colors = if (isSystemInDarkTheme()) DarkColors else LightColors) {
                MainPage(gameViewModel = GameViewModel())
            }
        }
    }
}

@SuppressLint("UnusedMaterialScaffoldPaddingParameter", "CoroutineCreationDuringComposition")
@ExperimentalAnimationApi
@ExperimentalFoundationApi1
@Composable
fun MainPage(gameViewModel: GameViewModel) {
    val scaffoldState = rememberScaffoldState()
    val snackbarCoroutineScope = rememberCoroutineScope()
    Scaffold(scaffoldState = scaffoldState) {
        Box(
            modifier = Modifier
                .padding(top = 50.dp, start = 10.dp, end = 10.dp, bottom = 0.dp)
                .fillMaxSize()
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                modifier = Modifier.fillMaxWidth()
            ) {
                Text(
                    "Lights out",
                    fontSize = 36.sp,
                    color = MaterialTheme.colors.primary,
                )
                Spacer(modifier = Modifier.height(18.dp))
                Text(
                    "A Jetpack Compose game based on 'Lights out with a Marvel-twist",
                    fontSize = 14.sp,
                    textAlign = TextAlign.Center,
                    color = MaterialTheme.colors.primary
                )
                Spacer(modifier = Modifier.height(14.dp))
                Box(
                    modifier = Modifier
                        .height(300.dp)
                        .width(300.dp),

                    ) {
                    GameGrid(gameViewModel)
                }
                Spacer(modifier = Modifier.height(8.dp))
                Column() {
                    Button(
                        onClick = {
                            gameViewModel.startGame()
                        },
                        colors = ButtonDefaults.textButtonColors(
                            backgroundColor = Color(red = 0f, green = 0f, blue = 1f),
                        )
                    ) {
                        Text(
                            text = if (gameViewModel.isStarted.value) "Randomize and start over" else "Start game",
                            color = Color(red = 1f, green = 1f, blue = 1f)
                        )
                    }
                }
                Spacer(modifier = Modifier.height(14.dp))
                Text(
                    "Click on a tile to adjust the state of tile and the directly adjacent ones.",
                    fontSize = 14.sp,
                    textAlign = TextAlign.Center,
                    color = MaterialTheme.colors.primary
                )
                Text(
                    "Keep on clicking to reveal the complete Marvel picture.",
                    fontSize = 14.sp,
                    textAlign = TextAlign.Center,
                    color = MaterialTheme.colors.primary
                )
                Text(
                    "Created by Marcofolio.net based on the electronic game 'Lights Out' from 1995 by Tiger Electronics. Picture used from Marvels 'Avengers: Infinity War' 2018 movie.",
                    fontSize = 8.sp,
                    textAlign = TextAlign.Center,
                    color = MaterialTheme.colors.primary
                )
                if(gameViewModel.isWon.value){
                    snackbarCoroutineScope.launch {
                        scaffoldState.snackbarHostState.showSnackbar("Well done, the Avengers are proud!")
                    }
                }
            }
        }
    }
}

@ExperimentalAnimationApi
@ExperimentalFoundationApi1
@Composable
fun GameGrid(gameViewModel: GameViewModel) {
    val list = (0..24).map { it }
    LazyVerticalGrid(
        columns = GridCells.Fixed(5)
    ) {
        items(list.count()) { item ->
            FlipCard(gameViewModel,  gameViewModel.playingField.value[item])
        }
    }
}

@ExperimentalAnimationApi
@Composable
fun FlipCard(gameViewModel: GameViewModel, tile: Tile) {
    val resourceId = Helpers.getResourceId(tile.row, tile.col)
    val rotation by animateFloatAsState(
        targetValue = if (!tile.imageVisible) 180f else 0f,
        animationSpec = tween(500)
    )
    val animateBack by animateFloatAsState(
        targetValue = if (!tile.imageVisible) 1f else 0f,
        animationSpec = tween(500)
    )
    Box(
        Modifier
            .height(60.dp)
            .width(60.dp),
        contentAlignment = Alignment.Center
    ) {
        Box(
            Modifier
                .graphicsLayer {
                    rotationY = rotation
                    cameraDistance = 8 * density
                }
                .clickable {
                    gameViewModel.flipTile(tile.row, tile.col)
                }
                .background(Color.Black.copy(alpha = animateBack)),
        )
        {
            Column(
                Modifier.fillMaxSize(),
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center,

                ) {
                AnimatedVisibility(
                    visible = rotation < 120f,
                ) {
                    Image(
                        ImageBitmap.imageResource(id = resourceId),
                        contentDescription = "",
                        modifier = Modifier
                            .height(60.dp)
                            .width(60.dp),
                        alignment = Alignment.Center,
                    )
                }
            }

        }
    }
}

@ExperimentalAnimationApi
@ExperimentalFoundationApi1
@Preview(name = "Light mode", showBackground = true)
@Preview(
    uiMode = Configuration.UI_MODE_NIGHT_YES,
    showBackground = true,
    name = "Dark Mode"
)
@Composable
fun PreviewMain() {
    val DarkColors = darkColors(
        primary = Color(red = 1f, green = 1f, blue = 1f),
    )
    val LightColors = lightColors(
        primary = Color(red = 0f, green = 0f, blue = 0f)
    )
    var game by remember { mutableStateOf(Game(isFinished = true)) }
    MaterialTheme(colors = if (isSystemInDarkTheme()) DarkColors else LightColors) {
        //MainPage(game)
    }
}
