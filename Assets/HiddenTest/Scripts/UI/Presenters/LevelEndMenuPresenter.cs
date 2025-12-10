using System;
using UnityEngine;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.UniCore.UI.Views.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters
{
    using Abstractions;

    public class LevelEndMenuPresenter : PresenterBase<ILevelEndMenuView>, ILevelEndMenuPresenter, IPresenter<ILevelEndMenuView>
    {
        public event Action Continue;
        public event Action Restart;
        public event Action MainMenu;
        public event Action Exit;

        public LevelEndMenuPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator) : base(viewFactory, navigator)
        {
        }

        protected override void Subscribe()
        {
            View.ContinueRequested += OnContinueRequested;
            View.RestartRequested += OnRestartRequested;
            View.MainMenuExitRequested += OnMainMenuExitRequested;
            View.ExitRequested += OnExitRequested;
            UpdateView();
        }

        protected override void Unsubscribe()
        {
            View.ContinueRequested -= OnContinueRequested;
            View.RestartRequested -= OnRestartRequested;
            View.MainMenuExitRequested -= OnMainMenuExitRequested;
            View.ExitRequested -= OnExitRequested;
        }

        private static readonly string _gameWinText = "You are a winner!";
        private static readonly string _levelWinText = "Level completed!";
        private static readonly string _levelLoseText = "You are a loser!";
        // Light orange color
        private static readonly Color _wonTitleTextColor = new(1, (float)0xAC / 0xFF, (float)0x40 / 0xFF, 1);
        // Orange red color
        private static readonly Color _lostTitleTextColor = new((float)0xE1 / 0xFF, (float)0x24 / 0xFF, 0);

        private bool _IsLevelWon => throw new NotImplementedException();
        private bool _IsLastLevel => throw new NotImplementedException();
        private int _LevelRating => throw new NotImplementedException();
        private int _MaxLevelRating => throw new NotImplementedException();
        private Sprite _RatingSprite => throw new NotImplementedException();
        private Sprite _HollowRatingSprite => throw new NotImplementedException();

        private void UpdateView()
        {
            if (_IsLevelWon)
            {
                View.TitleTextColor = _wonTitleTextColor;
                if (!_IsLastLevel)
                {
                    View.TitleText = _levelWinText;
                    View.EnableContinue = true;
                }
                else
                {
                    View.TitleText = _gameWinText;
                    View.EnableContinue = false;
                }
                for (int i = 0; i < _MaxLevelRating; i++)
                {
                    if (i < _LevelRating)
                    {
                        View.SetRatingIcon(i, _RatingSprite);
                    }
                    else
                    {
                        View.SetRatingIcon(i, _HollowRatingSprite);
                    }
                }
            }
            else
            {
                View.TitleText = _levelLoseText;
                View.TitleTextColor = _lostTitleTextColor;
                View.EnableContinue = false;
            }
        }

        private void OnContinueRequested() => Continue?.Invoke();

        private void OnRestartRequested() => Restart?.Invoke();

        private void OnMainMenuExitRequested() => MainMenu?.Invoke();

        private void OnExitRequested() => Exit?.Invoke();
    }
}
