using LightsOutPuzzle.Helpers;
using LightsOutPuzzle.ViewModels;
using LightsOutPuzzle.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightsOutPuzzle
{
    public partial class MainPage : ContentPage
    {
        private GameViewModel _model;
        private Grid _playingField;
        private TilePage[,] _tiles;

        public MainPage()
        {
            InitializeComponent();
            _model = new GameViewModel();
            _model.PropertyChanged += OnViewModelPropertyChanged;
            BindingContext = _model;
            _tiles = new TilePage[5, 5];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CreateLayout();
        }

        private void CreateLayout()
        {
            var layout = new StackLayout();

            // Subtitle
            var subtitle = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 20,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "A Xamarin.Forms game based on 'Lights Out' with a Marvel-twist."
            };

            // Playingfield
            _playingField = new Grid()
            {
                Opacity = 0.5,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 10,
                RowSpacing = Constants.TILE_PADDING,
                ColumnSpacing = Constants.TILE_PADDING,
            };

            for (var i = 0; i < 5; i++)
            {
                _playingField.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Constants.TILE_SIZE) });
                _playingField.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Constants.TILE_SIZE) });
            }

            // Create the tiles
            for (var row = 0; row < 5; row++)
            {
                for (var col = 0; col < 5; col++)
                {
                    var tile = new TilePage(row, col);
                    _tiles[row, col] = tile;
                    _playingField.Children.Add(tile, col, row);
                }
            }

            // Start game
            var startButton = new Button()
            {
                HorizontalOptions = LayoutOptions.Center
            };
            startButton.SetBinding(Button.CommandProperty, "StartCommand");
            startButton.SetBinding(Button.TextProperty, "ButtonText");

            // Rules
            var rules = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 20,
                Text = "Click on a tile to adjust the state of tile and the direct adject ones. Keep on clicking to reveal the complete Marvel picture."
            };

            // Notes
            var notes = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = 20,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Text = "Created by Marcofolio.net based on the electronic game 'Lights Out' from 1995 by Tiger Electronics. Picture used from Marvels 'Avengers: Infinity War' 2018 movie."
            };

            layout.Children.Add(subtitle);
            layout.Children.Add(_playingField);
            layout.Children.Add(startButton);
            layout.Children.Add(rules);
            layout.Children.Add(notes);

            Content = layout;
        }

        private async void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "IsFinished" && _model.IsFinished)
            {
                await DisplayAlert("Done", "Well done, the Avengers are proud!", "OK");
            }

            if (e.PropertyName == "IsActive")
            {
                if (_model.IsActive)
                {
                    await _playingField.FadeTo(1);
                }
                else
                {
                    await _playingField.FadeTo(0.5);
                }
            }

            if (e.PropertyName == "TilesToFlip")
            {
                await FlipTiles();
            }
        }

        private async Task FlipTiles()
        {
            _model.IsAnimating = true;
            var flips = new List<Task>();
            foreach (var t in _model.GetTiles())
            {
                var gameTile = _tiles[t.Row, t.Column];
                if (gameTile.FrontIsShowing != t.ImageVisible)
                {
                    flips.Add(gameTile.Flip());
                }
            }
            await Task.WhenAll(flips);
            _model.IsAnimating = false;
        }
    }
}
