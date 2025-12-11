using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;
    using log4net.Core;

    public class GameStateNavigator : IGameStateNavigator
    {
        public GameState State => _stateSwitcher.State;

        public GameStateNavigator(IStateSwitcher<GameState> stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void LoadMainMenu()
        {
            _stateSwitcher.TransitTo(GameState.Loading);
        }

        public void LoadLevel(int levelId)
        {
            _stateSwitcher.TransitTo(GameState.Loading, new LevelLoadContext(levelId));
        }

        public void PauseLevel()
        {
            _stateSwitcher.TransitTo(GameState.Pause);
        }

        public void ResumeLevel()
        {
            _stateSwitcher.TransitTo(GameState.LevelGameplay);
        }

        public void FinishLevel()
        {
            _stateSwitcher.TransitTo(GameState.LevelEnd);
        }

        public void Exit()
        {
            _stateSwitcher.TransitTo(GameState.Exit);
        }

        private readonly IStateSwitcher<GameState> _stateSwitcher;
    }
}
