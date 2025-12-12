using System.Collections.Generic;
using KarenKrill.UniCore.StateSystem.Abstractions;

namespace HiddenTest.GameFlow.Abstractions
{
    public class GameStateGraph : IStateGraph<GameState>
    {
        public GameState InitialState => GameState.Initial;
        public IDictionary<GameState, IList<GameState>> Transitions => _transitions;

        private readonly IDictionary<GameState, IList<GameState>> _transitions = new Dictionary<GameState, IList<GameState>>()
        {
            { GameState.Initial, new List<GameState> { GameState.Loading, GameState.Exit } },
            { GameState.Loading, new List<GameState> { GameState.MainMenu, GameState.LevelGameplay, GameState.Exit } },
            { GameState.MainMenu, new List<GameState> { GameState.Loading, GameState.Exit } },
            { GameState.LevelGameplay, new List<GameState> { GameState.Pause, GameState.LevelEnd, GameState.Exit } },
            { GameState.Pause, new List<GameState> { GameState.LevelGameplay, GameState.LevelEnd, GameState.Loading, GameState.Exit } },
            { GameState.LevelEnd, new List<GameState> { GameState.Loading, GameState.Exit } },
            { GameState.Exit, new List<GameState>() }
        };
    }
}
