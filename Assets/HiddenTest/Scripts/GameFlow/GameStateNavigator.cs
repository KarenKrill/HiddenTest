using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class GameStateNavigator : IGameStateNavigator
    {
        public GameState CurrentState => _stateSwitcher.State;

        public GameStateNavigator(IStateSwitcher<GameState> stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void LoadMainMenu()
        {
            _stateSwitcher.TransitTo(GameState.MainMenu);
        }

        public void LoadLevel(int levelId)
        {
            _stateSwitcher.TransitTo(GameState.LevelGameplay, levelId);
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
