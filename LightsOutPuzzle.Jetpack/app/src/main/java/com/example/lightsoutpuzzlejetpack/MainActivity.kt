package com.example.lightsoutpuzzlejetpack

import android.content.res.Configuration
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.animation.animateColorAsState
import androidx.compose.animation.core.animateFloatAsState
import androidx.compose.animation.core.tween
import androidx.compose.foundation.*
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.lightsoutpuzzlejetpack.R.drawable.*
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.focus.focusModifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.ImageBitmap
import androidx.compose.ui.graphics.graphicsLayer
import androidx.compose.ui.res.imageResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.sp
import java.util.*

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
            Text(
                "Lights out",
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
                    .height(300.dp)
                    .width(300.dp),

            ){
                GameGrid()
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
@Composable
fun GameGrid(){
    val list = (0..24).map { it }

    LazyVerticalGrid(
        cells = GridCells.Fixed(5)
    ) {
        items(list) { item ->
            var resourceId = getResourceId(item)
            var img = Image(
                ImageBitmap.imageResource(id = resourceId),
                contentDescription = "",
                modifier = Modifier.height(60.dp).width(60.dp),
                alignment = Alignment.Center,
            )
            FlipCard(img)
        }
    }
}

fun getResourceId(item:Int): Int {
    var col = item%5
    var row = item/5
    when (row) {
        0 -> when (col) {
            0 -> return R.drawable.row_1_col_1
            1 -> return R.drawable.row_1_col_2
            2 -> return R.drawable.row_1_col_3
            3 -> return R.drawable.row_1_col_4
            else ->
                return R.drawable.row_1_col_5
        }
        1 -> when (col) {
            0 -> return R.drawable.row_2_col_1
            1 -> return R.drawable.row_2_col_2
            2 -> return R.drawable.row_2_col_3
            3 -> return R.drawable.row_2_col_4
            else ->
                return R.drawable.row_2_col_5
        }
        2 -> when (col) {
            0 -> return R.drawable.row_3_col_1
            1 -> return R.drawable.row_3_col_2
            2 -> return R.drawable.row_3_col_3
            3 -> return R.drawable.row_3_col_4
            else ->
                return R.drawable.row_3_col_5
        }
        3 -> when (col) {
            0 -> return R.drawable.row_4_col_1
            1 -> return R.drawable.row_4_col_2
            2 -> return R.drawable.row_4_col_3
            3 -> return R.drawable.row_4_col_4
            else ->
                return R.drawable.row_4_col_5
        }
        else ->
            when (col) {
                0 -> return R.drawable.row_5_col_1
                1 -> return R.drawable.row_5_col_2
                2 -> return R.drawable.row_5_col_3
                3 -> return R.drawable.row_5_col_4
                else ->
                    return R.drawable.row_5_col_5
            }

    }

}

@Composable
fun FlipCard(image: Unit) {
    var rotated by remember { mutableStateOf(false) }
    val rotation by animateFloatAsState(
        targetValue = if (rotated) 180f else 0f,
        animationSpec = tween(500)
    )
    val animateFront by animateFloatAsState(
        targetValue = if (!rotated) 1f else 0f,
        animationSpec = tween(100)
    )
    val animateBack by animateFloatAsState(
        targetValue = if (rotated) 1f else 0f,
        animationSpec = tween(100)
    )
    val animateColor by animateColorAsState(
        targetValue = if(rotated) Color.Black else Color.Black.copy(alpha = 0f),
        animationSpec = tween(200)
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
                    rotated = !rotated
                }
                .background(animateColor),
        )
        {
            Column(
                Modifier.fillMaxSize(),
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center
            ) {
                if(rotated)
                {
                    Text(text = "Back",
                        modifier = Modifier
                            .graphicsLayer {
                                rotationY = rotation
                            })
                } else{
                    image
                }

            }

        }
    }
}
fun getColor() : Color {
    val rnd = Random()
    val color = Color(255, rnd.nextInt(256), rnd.nextInt(256))
    return color
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