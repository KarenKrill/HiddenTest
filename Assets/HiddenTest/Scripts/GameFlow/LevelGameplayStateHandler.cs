#nullable enable
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class LevelGameplayStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.LevelGameplay;

        public LevelGameplayStateHandler()
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
