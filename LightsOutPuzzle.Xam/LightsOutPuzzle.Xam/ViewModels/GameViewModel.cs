using LightsOutPuzzle.Common;
using LightsOutPuzzle.Xam.Helpers;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace LightsOutPuzzle.Xam.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private Game _game;
        private IEnumerable<Tile> _tilesToFlip;
        private bool _isActive;
        private bool _isFinished;
        private string _buttonText;
        private bool _isAnimating;

        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        public bool IsAnimating
        {
            get { return _isAnimating; }
            set { SetProperty(ref _isAnimating, value); }
        }

        public bool IsFinished
        {
            get { return _isFinished; }
            set { SetProperty(ref _isFinished, value); }
        }

        public IEnumerable<Tile> TilesToFlip
        {
            get { return _tilesToFlip; }
            set { SetProperty(ref _tilesToFlip, value); }
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set { SetProperty(ref _buttonText, value); }
        }

        public ICommand StartCommand { get; private set; }

        public ICommand TileTappedCommand { get; private set; }

        public GameViewModel()
        {
            _game = Game.FinishedGame();
            ButtonText = "Start game";
            StartCommand = new Command(StartGame);
            TileTappedCommand = new Command<TileTappedEventArgs>(TileTapped);
        }

        public IEnumerable<Tile> GetTiles() =>
            _game.GetAll();

        private void StartGame()
        {
            ButtonText = "Randomize & start over";
            IsFinished = false;
            IsActive = true;
            _game = Game.RandomGame();
            TilesToFlip = _game.GetFlippedTiles();
        }

        private void TileTapped(TileTappedEventArgs e)
        {
            if (!IsActive || IsAnimating)
            {
                return;
            }
            TilesToFlip = _game.FlipTile(e.Row, e.Column);
            if (_game.GameCompleted())
            {
                PuzzleComplete();
            }
        }

        private void PuzzleComplete()
        {
            IsFinished = true;
            IsActive = false;
        }
    }
}
