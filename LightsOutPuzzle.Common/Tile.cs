namespace LightsOutPuzzle.Common
{
    public class Tile
    {
        public Tile(int row, int column, bool imageVisible = false)
        {
            Row = row;
            Column = column;
            ImageVisible = imageVisible;
        }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool ImageVisible { get; set; }
        public void Flip()
        {
            ImageVisible = !ImageVisible;
        }
    }
}
