using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Scriptable Objects/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        public IReadOnlyList<ILevelConfig> LevelsConfig => _levelsConfig;

        [SerializeField]
        private List<LevelConfig> _levelsConfig = new();
    }
}
