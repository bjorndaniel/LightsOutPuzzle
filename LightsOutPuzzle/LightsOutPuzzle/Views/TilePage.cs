using LightsOutPuzzle.Helpers;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightsOutPuzzle.Views
{
    public class TilePage : Grid
    {
        private BoxView _background;
        private Image _foreground;

        public int Column { get; }

        public int Row { get; }

        public bool FrontIsShowing { get; private set; }

        public TilePage(int row, int column)
        {
            Column = column;
            Row = row;
            // Background
            _background = new BoxView { Color = Constants.BACKSIZE_COLOR };
            var assembly = typeof(TilePage).GetTypeInfo().Assembly;
            // Foreground
            _foreground = new Image
            {
                RotationY = -90,
                Source = ImageSource.FromResource($"LightsOutPuzzle.Images.row-{row + 1}-col-{column + 1}.jpg", assembly),
                Opacity = 0,
                BackgroundColor = Color.Red

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
                ShowBack();
            }
            else
            {
                await _background.RotateYTo(90, 400, Easing.CubicIn);
                _foreground.Opacity = 1;
                await _foreground.RotateYTo(0, 400, Easing.CubicOut);
                FrontIsShowing = true;
            }
        }

        public async Task Reset()
        {
            if (FrontIsShowing)
            {
                await ShowBack();
            }
        }

        private async Task ShowBack()
        {
            await _foreground.RotateYTo(-90, 400, Easing.CubicIn);
            _foreground.Opacity = 0;
            await _background.RotateYTo(0, 400, Easing.CubicOut);
            FrontIsShowing = false;
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
            var tgr = new TapGestureRecognizer();
            tgr.SetBinding(TapGestureRecognizer.CommandProperty, "TileTappedCommand");
            tgr.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding { Source = new TileTappedEventArgs { Row = Row, Column = Column } });
            frame.GestureRecognizers.Add(tgr);
            return frame;
        }
    }
}
