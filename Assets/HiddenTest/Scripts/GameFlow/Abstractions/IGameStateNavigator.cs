namespace HiddenTest.GameFlow.Abstractions
{
    public interface IGameStateNavigator
    {
        GameState CurrentState { get; }

        void LoadMainMenu();
        void LoadLevel(int levelId);
        void FinishLevel();
        void Exit();
    }
}
