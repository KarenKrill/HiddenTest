using System.Collections.Generic;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IGameConfig
    {
        public IReadOnlyList<ILevelConfig> LevelsConfig { get; }
        public int ActiveLevel { get; set; }
        public float PauseTimeScale { get; }
    }
}
