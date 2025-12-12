using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface ILevelConfig
    {
        public TaskViewType TaskViewType { get; }
        public int MaxTaskCount { get; }
        public bool TimeLimitEnabled { get; }
        public float TimeLimitInSeconds { get; }
        public IReadOnlyDictionary<int, IItemConfig> EnabledItems { get; }
        public AudioClip BackgroundMusic { get; }
        public GameObject LevelPrefab { get; }
    }
}
