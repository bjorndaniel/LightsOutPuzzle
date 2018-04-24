using System;

namespace LightsOutPuzzle.Helpers
{
    public class TileTappedEventArgs : EventArgs
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}
