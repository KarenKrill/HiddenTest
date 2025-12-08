#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class LevelGameplayStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.LevelGameplay;

        public LevelGameplayStateHandler(IGameStateNavigator gameStateNavigator,
            IInGameMenuPresenter inGameMenuPresenter,
            IPauseMenuPresenter pauseMenuPresenter) : base(inGameMenuPresenter)
        {
            _gameStateNavigator = gameStateNavigator;
            _inGameMenuPresenter = inGameMenuPresenter;
            _pauseMenuPresenter = pauseMenuPresenter;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
            _inGameMenuPresenter.Pause += OnPauseRequested;
            _pauseMenuPresenter.Resume += OnResumeRequested;
            _pauseMenuPresenter.Restart += OnRestartRequested;
            _pauseMenuPresenter.MainMenu += OnMainMenuRequested;
            _pauseMenuPresenter.Exit += OnExitRequested;
        }

        public override void Exit(GameState nextState)
        {
            _inGameMenuPresenter.Pause -= OnPauseRequested;
            _pauseMenuPresenter.Resume -= OnResumeRequested;
            _pauseMenuPresenter.Disable();
            base.Exit(nextState);
        }

        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly IInGameMenuPresenter _inGameMenuPresenter;
        private readonly IPauseMenuPresenter _pauseMenuPresenter;

        private void OnPauseRequested() => _pauseMenuPresenter.Enable();
        private void OnResumeRequested() => _pauseMenuPresenter.Disable();
        private void OnRestartRequested() => _gameStateNavigator.LoadLevel(0);
        private void OnMainMenuRequested() => _gameStateNavigator.LoadMainMenu();
        private void OnExitRequested() => _gameStateNavigator.Exit();

    }
}
