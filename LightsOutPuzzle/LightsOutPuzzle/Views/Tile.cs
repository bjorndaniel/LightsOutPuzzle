using LightsOutPuzzle.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using System.Diagnostics;

namespace LightsOutPuzzle.Views
{
    public class Tile : Grid
    {

        private BoxView _background;
        private Image _foreground;

        public event EventHandler<TileTappedEventArgs> Tapped;

        public int XPos { get; }

        public int YPos { get; }

        public bool FrontIsShowing { get; private set; }

        public Tile(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
            // Background
            _background = new BoxView { Color = Constants.BACKSIZE_COLOR };
            var assembly = typeof(Tile).GetTypeInfo().Assembly;
            // Foreground
            _foreground = new Image
            {
                RotationY = -90,
                Source = ImageSource.FromResource($"LightsOutPuzzle.Images.row-{yPos + 1}-col-{xPos + 1}.jpg", assembly),
                Opacity = 0,
                BackgroundColor = Color.Black
            
            };
            // Tapframe
            var tapFrame = CreateTapFrame();
            // The Background, Foreground and tapGrid are placed in the same Cell of the Grid
            // which causes them to stack on top of eachother
            Children.Add(_background, 0, 0);
            Children.Add(_foreground, 0, 0);
            Children.Add(tapFrame, 0, 0);
        }

        public async Task Flip()
        {
            if (FrontIsShowing)
            {
                await _foreground.RotateYTo(-90, 400, Easing.CubicIn);
                _foreground.Opacity = 0;
                await _background.RotateYTo(0, 400, Easing.CubicOut);
                FrontIsShowing = false;
            }
            else
            {
                await _background.RotateYTo(90, 400, Easing.CubicIn);
                _foreground.Opacity = 1;
                await _foreground.RotateYTo(0, 400, Easing.CubicOut);
                FrontIsShowing = true;
            }
        }

        private Frame CreateTapFrame()
        {
            var frame = new Frame()
            {
                WidthRequest = Constants.TILE_SIZE,
                HeightRequest = Constants.TILE_SIZE,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HasShadow = false
            };
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnFrameTapped;
            frame.GestureRecognizers.Add(tapGestureRecognizer);
            return frame;
        }

        protected virtual void OnFrameTapped(object sender, EventArgs e)
        {
            Tapped?.Invoke(this, new TileTappedEventArgs { XPos = XPos, YPos = YPos });
        }
    }
}
