using System.Collections.Generic;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface ILevelConfig
    {
        public TaskViewType TaskViewType { get; }
        public bool TimeLimitEnabled { get; }
        public float TimeLimitInSeconds { get; }
        public IReadOnlyList<IItemConfig> Items { get; }
    }
}
