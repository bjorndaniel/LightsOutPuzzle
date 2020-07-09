namespace LightsOutPuzzle.Common
{
    public class Tile
    {
        public Tile(int row, int column, bool imageVisible = false)
        {
            Row = row;
            Column = column;
            ImageVisible = imageVisible;
            PreviousState = imageVisible;
        }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool ImageVisible { get; set; }
        public bool PreviousState { get; set; }
        public bool HasBeenFlipped => PreviousState != ImageVisible;
        public void Flip()
        {
            PreviousState = ImageVisible;
            ImageVisible = !ImageVisible;
        }
    }
}
