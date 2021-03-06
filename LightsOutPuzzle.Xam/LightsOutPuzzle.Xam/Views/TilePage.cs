﻿using LightsOutPuzzle.Xam.Helpers;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightsOutPuzzle.Xam.Views
{
    public class TilePage : Grid
    {
        private Image _background;
        private Image _foreground;

        public int Column { get; }

        public int Row { get; }

        public bool FrontIsShowing { get; private set; }

        public TilePage(int row, int column, string assemblyName)
        {
            Column = column;
            Row = row;
            var assembly = typeof(TilePage).GetTypeInfo().Assembly;
            _background = new Image
            {
                Source = ImageSource.FromResource($"{assemblyName}.Images.icon.png", assembly),
                RotationY = 90,
                BackgroundColor = Color.White
            };
            _foreground = new Image
            {
                RotationY = 0,
                Source = ImageSource.FromResource($"{assemblyName}.Images.row-{row + 1}-col-{column + 1}.jpg", assembly),
                BackgroundColor = Color.Transparent

            };

            var tapFrame = CreateTapFrame();
            Children.Add(_background, 0, 0);
            Children.Add(_foreground, 0, 0);
            Children.Add(tapFrame, 0, 0);
            FrontIsShowing = true;
        }

        public async Task Flip()
        {
            if (FrontIsShowing)
            {
                await ShowBack().ConfigureAwait(false);
            }
            else
            {
                await _background.RotateYTo(90, 200, Easing.CubicIn).ConfigureAwait(false);
                await _foreground.RotateYTo(0, 200, Easing.CubicOut).ConfigureAwait(false);
                FrontIsShowing = true;
            }
        }

        public async Task Reset()
        {
            if (FrontIsShowing)
            {
                await ShowBack().ConfigureAwait(false);
            }
        }

        private async Task ShowBack()
        {
            await _foreground.RotateYTo(-90, 400, Easing.CubicIn).ConfigureAwait(false);
            await _background.RotateYTo(0, 400, Easing.CubicOut).ConfigureAwait(false);
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