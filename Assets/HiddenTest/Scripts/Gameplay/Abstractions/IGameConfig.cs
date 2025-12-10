using System.Collections.Generic;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IGameConfig
    {
        public IReadOnlyList<ILevelConfig> LevelsConfig { get; }
    }
}
