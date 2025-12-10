#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;
    using HiddenTest.UI.Presenters.Abstractions;

    public class LevelEndStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.LevelEnd;

        public LevelEndStateHandler(ILevelEndMenuPresenter levelEndMenuPresenter) : base(levelEndMenuPresenter)
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
