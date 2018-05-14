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
            StyleId = "flip-container";
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
                //Opacity = 0,
                BackgroundColor = Color.Red

            };
            _background.StyleId = "back";
            _foreground.StyleId = "front";
            // Tapframe
            var tapFrame = CreateTapFrame();
            // The Background, Foreground and tapGrid are placed in the same Cell of the Grid
            // which causes them to stack on top of eachother
            //var g = new Grid();
            //g.StyleId = "flipper";
            Children.Add(_background, 0, 0);
            Children.Add(_foreground, 0, 0);
            //Children.Add(g);
            Children.Add(tapFrame, 0, 0);
        }

        public async Task Flip()
        {
            if (FrontIsShowing)
            {
               await ShowBack();
            }
            else
            {
                await _background.RotateYTo(90, 400, Easing.CubicIn);
                await _foreground.RotateYTo(0, 400, Easing.CubicOut);
                _background.StyleId = "back";
                _foreground.StyleId = "front";
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
            await _background.RotateYTo(0, 400, Easing.CubicOut);
            _background.StyleId = "front";
            _foreground.StyleId = "back";
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
                HasShadow = false,
            };
            var tgr = new TapGestureRecognizer();
            tgr.SetBinding(TapGestureRecognizer.CommandProperty, "TileTappedCommand");
            tgr.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding { Source = new TileTappedEventArgs { Row = Row, Column = Column } });
            frame.GestureRecognizers.Add(tgr);
            return frame;
        }
    }
}
