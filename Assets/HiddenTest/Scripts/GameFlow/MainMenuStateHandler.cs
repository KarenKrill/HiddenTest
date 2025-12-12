#nullable enable
using UnityEngine;
using KarenKrill.Audio.Abstractions;
using KarenKrill.UniCore.StateSystem.Abstractions;
using HiddenTest.Gameplay.Abstractions;
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
            IAudioController audioController,
            IGameConfig gameConfig) : base(mainMenuPresenter)
        {
            _logger = logger;
            _gameStateNavigator = gameStateNavigator;
            _mainMenuPresenter = mainMenuPresenter;
            _audioController = audioController;
            _gameConfig = gameConfig;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
            _mainMenuPresenter.NewGame += OnNewGame;
            _mainMenuPresenter.Exit += OnExit;
            if (_gameConfig.MenuBackgroundMusic != null)
            {
                _audioController.PlayMusic(_gameConfig.MenuBackgroundMusic);
            }
            _logger.Log($"{nameof(MainMenuStateHandler)}.{nameof(Enter)}()");
        }

        public override void Exit(GameState nextState)
        {
            _mainMenuPresenter.NewGame -= OnNewGame;
            _mainMenuPresenter.Exit -= OnExit;
            _audioController.StopMusic();
            base.Exit(nextState);
            _logger.Log($"{nameof(MainMenuStateHandler)}.{nameof(Exit)}()");
        }

        private readonly ILogger _logger;
        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly IMainMenuPresenter _mainMenuPresenter;
        private readonly IAudioController _audioController;
        private readonly IGameConfig _gameConfig;

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
