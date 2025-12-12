#nullable enable
using HiddenTest.Input.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;
using KarenKrill.UniCore.Input.Abstractions;
using KarenKrill.UniCore.StateSystem.Abstractions;
using UnityEngine;

namespace HiddenTest.GameFlow
{
    using Abstractions;
    using HiddenTest.Gameplay.Abstractions;

    public class PauseStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.Pause;

        public PauseStateHandler(ILogger logger,
            IGameConfig gameConfig,
            ILevelSession levelSession,
            IGameStateNavigator gameStateNavigator,
            IBasicActionsProvider actionsProvider,
            IUIActionsProvider uiActionsProvider,
            IPauseMenuPresenter pauseMenuPresenter)
            : base(pauseMenuPresenter)
        {
            _logger = logger;
            _gameConfig = gameConfig;
            _levelSession = levelSession;
            _gameStateNavigator = gameStateNavigator;
            _actionsProvider = actionsProvider;
            _uiActionsProvider = uiActionsProvider;
            _pauseMenuPresenter = pauseMenuPresenter;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
            _previousTimeScale = Time.timeScale;
            Time.timeScale = _gameConfig.PauseTimeScale;
            _logger.Log(nameof(PauseStateHandler), nameof(Enter));
            _uiActionsProvider.Cancel += OnResumeRequested;
            _pauseMenuPresenter.Resume += OnResumeRequested;
            _pauseMenuPresenter.Restart += OnRestartRequested;
            _pauseMenuPresenter.MainMenu += OnMainMenu;
            _pauseMenuPresenter.Exit += OnExit;
            _actionsProvider.SetActionMap(ActionMap.UI);
        }

        public override void Exit(GameState nextState)
        {
            _logger.Log(nameof(PauseStateHandler), nameof(Exit));
            _uiActionsProvider.Cancel -= OnResumeRequested;
            _pauseMenuPresenter.Resume -= OnResumeRequested;
            _pauseMenuPresenter.Restart -= OnRestartRequested;
            _pauseMenuPresenter.MainMenu -= OnMainMenu;
            _pauseMenuPresenter.Exit -= OnExit;
            Time.timeScale = _previousTimeScale;
            base.Exit(nextState);
        }

        private readonly ILogger _logger;
        private readonly IGameConfig _gameConfig;
        private readonly ILevelSession _levelSession;
        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly IBasicActionsProvider _actionsProvider;
        private readonly IUIActionsProvider _uiActionsProvider;
        private readonly IPauseMenuPresenter _pauseMenuPresenter;
        private float _previousTimeScale;

        private void OnResumeRequested()
        {
            _gameStateNavigator.ResumeLevel();
        }

        private void OnRestartRequested()
        {
            _gameStateNavigator.LoadLevel(_levelSession.ActiveLevel);
        }

        private void OnMainMenu() => _gameStateNavigator.LoadMainMenu();

        private void OnExit() => _gameStateNavigator.Exit();

    }
}
