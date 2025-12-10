#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class LevelGameplayStateHandler : LevelGameplayStateHandlerBase, IStateHandler<GameState>
    {
        public LevelGameplayStateHandler(IGameStateNavigator gameStateNavigator,
            IInGameMenuPresenter inGameMenuPresenter,
            IPauseMenuPresenter pauseMenuPresenter) :
            base(gameStateNavigator, inGameMenuPresenter, pauseMenuPresenter)
        {
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
        }

        public override void Exit(GameState nextState)
        {
            base.Exit(nextState);
        }
    }
}
