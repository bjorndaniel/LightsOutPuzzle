using System;

namespace LightsOutPuzzle.Xam.Helpers
{
    public class TileTappedEventArgs : EventArgs
    {
        public int Column { get; set; }
        public int Row { get; set; }
    }
}
