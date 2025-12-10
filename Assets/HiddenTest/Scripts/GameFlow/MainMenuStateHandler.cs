#nullable enable
using UnityEngine;
using KarenKrill.UniCore.StateSystem.Abstractions;
using KarenKrill.Audio.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class MainMenuStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.MainMenu;

        public MainMenuStateHandler(ILogger logger,
            IGameStateNavigator gameStateNavigator,
            IMainMenuPresenter mainMenuPresenter,
            IAudioController audioController) : base(mainMenuPresenter)
        {
            _logger = logger;
            _gameStateNavigator = gameStateNavigator;
            _mainMenuPresenter = mainMenuPresenter;
            _audioController = audioController;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            _mainMenuPresenter.NewGame += OnNewGame;
            _mainMenuPresenter.Exit += OnExit;
            base.Enter(prevState);
            _logger.Log($"{nameof(MainMenuStateHandler)}.{nameof(Enter)}()");
        }

        public override void Exit(GameState nextState)
        {
            base.Exit(nextState);
            _mainMenuPresenter.NewGame -= OnNewGame;
            _mainMenuPresenter.Exit -= OnExit;
            _audioController.StopMusic();
            _logger.Log($"{nameof(MainMenuStateHandler)}.{nameof(Exit)}()");
        }

        private readonly ILogger _logger;
        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly IMainMenuPresenter _mainMenuPresenter;
        private readonly IAudioController _audioController;

        private void OnExit()
        {
            _gameStateNavigator.Exit();
        }

        private void OnNewGame()
        {
            _gameStateNavigator.LoadLevel(0);
        }
    }
}
