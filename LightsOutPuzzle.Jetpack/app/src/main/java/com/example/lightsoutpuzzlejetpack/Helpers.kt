package com.example.lightsoutpuzzlejetpack

class Helpers {
    companion object Factory {
        fun getResourceId(row: Int, col: Int): Int {
            when (row) {
                0 -> return when (col) {
                    0 -> R.drawable.row_1_col_1
                    1 -> R.drawable.row_1_col_2
                    2 -> R.drawable.row_1_col_3
                    3 -> R.drawable.row_1_col_4
                    else ->
                        R.drawable.row_1_col_5
                }
                1 -> return when (col) {
                    0 -> R.drawable.row_2_col_1
                    1 -> R.drawable.row_2_col_2
                    2 -> R.drawable.row_2_col_3
                    3 -> R.drawable.row_2_col_4
                    else ->
                        R.drawable.row_2_col_5
                }
                2 -> return when (col) {
                    0 -> R.drawable.row_3_col_1
                    1 -> R.drawable.row_3_col_2
                    2 -> R.drawable.row_3_col_3
                    3 -> R.drawable.row_3_col_4
                    else ->
                        R.drawable.row_3_col_5
                }
                3 -> return when (col) {
                    0 -> R.drawable.row_4_col_1
                    1 -> R.drawable.row_4_col_2
                    2 -> R.drawable.row_4_col_3
                    3 -> R.drawable.row_4_col_4
                    else ->
                        R.drawable.row_4_col_5
                }
                else ->
                    return when (col) {
                        0 -> R.drawable.row_5_col_1
                        1 -> R.drawable.row_5_col_2
                        2 -> R.drawable.row_5_col_3
                        3 -> R.drawable.row_5_col_4
                        else ->
                            R.drawable.row_5_col_5
                    }

            }
        }
    }
}