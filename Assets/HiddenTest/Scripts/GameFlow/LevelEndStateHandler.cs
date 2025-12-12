#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;
    using HiddenTest.Gameplay.Abstractions;
    using HiddenTest.UI.Presenters.Abstractions;

    public class LevelEndStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.LevelEnd;

        public LevelEndStateHandler(IGameStateNavigator gameStateNavigator,
            ILevelSession levelSession,
            ILevelEndMenuPresenter levelEndMenuPresenter) : base(levelEndMenuPresenter)
        {
            _gameStateNavigator = gameStateNavigator;
            _levelSession = levelSession;
            _levelEndMenuPresenter = levelEndMenuPresenter;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
            _levelEndMenuPresenter.Continue += OnContinue;
            _levelEndMenuPresenter.Restart += OnRestart;
            _levelEndMenuPresenter.MainMenu += OnMainMenu;
            _levelEndMenuPresenter.Exit += OnExit;
        }

        public override void Exit(GameState nextState)
        {
            _levelEndMenuPresenter.Continue += OnContinue;
            _levelEndMenuPresenter.Restart += OnRestart;
            _levelEndMenuPresenter.MainMenu += OnMainMenu;
            _levelEndMenuPresenter.Exit += OnExit;
            base.Exit(nextState);
        }

        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly ILevelSession _levelSession;
        private readonly ILevelEndMenuPresenter _levelEndMenuPresenter;

        private void OnContinue() => _gameStateNavigator.LoadLevel(_levelSession.ActiveLevel + 1);
        private void OnRestart() => _gameStateNavigator.LoadLevel(_levelSession.ActiveLevel);
        private void OnMainMenu() => _gameStateNavigator.LoadMainMenu();
        private void OnExit() => _gameStateNavigator.Exit();
    }
}
