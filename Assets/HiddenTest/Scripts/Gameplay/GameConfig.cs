using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Scriptable Objects/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        public IReadOnlyList<ILevelConfig> LevelsConfig => _levelsConfig;

        public int ActiveLevel { get; set; } = 0;

        [field: SerializeField, Range(0, 1)]
        public float PauseTimeScale { get; private set; } = 1f;

        [SerializeField]
        private List<LevelConfig> _levelsConfig = new();
    }
}
