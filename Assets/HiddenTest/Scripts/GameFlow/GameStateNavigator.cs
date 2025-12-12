#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class GameStateNavigator : IGameStateNavigator
    {
        public GameState State => _stateSwitcher.State;

        public event StateChangingHandler? StateChanging;
        public event StateChangedHandler? StateChanged;

        public GameStateNavigator(IStateSwitcher<GameState> stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void LoadMainMenu() => TransitTo(GameState.Loading);
        public void LoadLevel(int levelId) => TransitTo(GameState.Loading, new LevelLoadContext(levelId));
        public void PauseLevel() => TransitTo(GameState.Pause);
        public void ResumeLevel() => TransitTo(GameState.LevelGameplay);
        public void FinishLevel() => TransitTo(GameState.LevelEnd);
        public void Exit() => TransitTo(GameState.Exit);

        private readonly IStateSwitcher<GameState> _stateSwitcher;

        private void TransitTo(GameState state, object? context = null)
        {
            StateChanging?.Invoke(_stateSwitcher.State, state);
            _stateSwitcher.TransitTo(state, context);
            StateChanged?.Invoke(state);
        }
    }
}
