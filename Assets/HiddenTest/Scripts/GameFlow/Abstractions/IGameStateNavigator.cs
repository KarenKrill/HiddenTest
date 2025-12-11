namespace HiddenTest.GameFlow.Abstractions
{
    public interface IGameStateNavigator
    {
        GameState State { get; }

        void LoadMainMenu();
        void LoadLevel(int levelId);
        void PauseLevel();
        void ResumeLevel();
        void FinishLevel();
        void Exit();
    }
}
