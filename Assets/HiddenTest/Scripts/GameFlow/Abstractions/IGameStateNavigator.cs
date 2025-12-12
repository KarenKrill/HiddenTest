#nullable enable

namespace HiddenTest.GameFlow.Abstractions
{
    public delegate void StateChangingHandler(GameState fromState, GameState toState);
    public delegate void StateChangedHandler(GameState state);

    public interface IGameStateNavigator
    {
        GameState State { get; }

        event StateChangingHandler? StateChanging;
        event StateChangedHandler? StateChanged;

        void LoadMainMenu();
        void LoadLevel(int levelId);
        void PauseLevel();
        void ResumeLevel();
        void FinishLevel();
        void Exit();
    }
}
