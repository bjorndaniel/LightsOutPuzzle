package com.example.lightsoutpuzzlejetpack

import android.content.res.Configuration
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.*
import androidx.compose.animation.core.animateFloatAsState
import androidx.compose.animation.core.tween
import androidx.compose.foundation.*
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyVerticalGrid
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
import com.example.lightsoutpuzzlejetpack.R.drawable.*
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
                MainPage()
            }
        }
    }
}

@ExperimentalAnimationApi
@ExperimentalFoundationApi1
@Composable
fun MainPage() {
    var game by remember { mutableStateOf(Game(isFinished = true)) }
    var buttonText by remember { mutableStateOf("Start game") }
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
                GameGrid(game)
            }
            Spacer(modifier = Modifier.height(8.dp))
            Column() {
                Button(
                    onClick = {
                        game = Game(isFinished = false)
                        buttonText = if (game.isStarted) "Restart game" else "Start game"
                    },
                    colors = ButtonDefaults.textButtonColors(
                        backgroundColor = Color(red = 0f, green = 0f, blue = 1f),
                    )
                ) {
                    Text(text = buttonText, color = Color(red = 1f, green = 1f, blue = 1f))
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
        }
    }
}

@ExperimentalAnimationApi
@ExperimentalFoundationApi1
@Composable
fun GameGrid(game: Game) {
    val list = (0..24).map { it }

    LazyVerticalGrid(
        cells = GridCells.Fixed(5)
    ) {
        items(list) { item ->
            FlipCard(item / 5, item % 5, game)
        }
    }
}

fun getResourceId(row: Int, col: Int): Int {
    when (row) {
        0 -> return when (col) {
            0 -> row_1_col_1
            1 -> row_1_col_2
            2 -> row_1_col_3
            3 -> row_1_col_4
            else ->
                row_1_col_5
        }
        1 -> return when (col) {
            0 -> row_2_col_1
            1 -> row_2_col_2
            2 -> row_2_col_3
            3 -> row_2_col_4
            else ->
                row_2_col_5
        }
        2 -> return when (col) {
            0 -> row_3_col_1
            1 -> row_3_col_2
            2 -> row_3_col_3
            3 -> row_3_col_4
            else ->
                row_3_col_5
        }
        3 -> return when (col) {
            0 -> row_4_col_1
            1 -> row_4_col_2
            2 -> row_4_col_3
            3 -> row_4_col_4
            else ->
                row_4_col_5
        }
        else ->
            return when (col) {
                0 -> row_5_col_1
                1 -> row_5_col_2
                2 -> row_5_col_3
                3 -> row_5_col_4
                else ->
                    row_5_col_5
            }

    }
}

@ExperimentalAnimationApi
@Composable
fun FlipCard(row: Int, col: Int, game: Game) {
    val resourceId = getResourceId(row, col)
    val myRow = row
    val myCol = col
    var tile = game.getTile(row, col)
    val rotated = remember { mutableStateOf(true) }
    val rotation by animateFloatAsState(
        targetValue = if (!rotated.value) 180f else 0f,
        animationSpec = tween(500)
    )
    val animateBack by animateFloatAsState(
        targetValue = if (!rotated.value) 1f else 0f,
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
                    //tile.imageVisible = !tile.imageVisible
                    rotated.value = !rotated.value
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

fun flipTile(game: MutableState<Game>, tile: Tile): Boolean {
    tile.flip()
    return tile.imageVisible
    // val tiles = game.value.flipTile(tile.row, tile.col)
    // for (t in tiles) {
    //     t.flip()
    // }
}