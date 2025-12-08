namespace HiddenTest.GameFlow.Abstractions
{
    public interface IGameStateNavigator
    {
        GameState State { get; }

        void LoadMainMenu();
        void LoadLevel(int levelId);
        void FinishLevel();
        void Exit();
    }
}
