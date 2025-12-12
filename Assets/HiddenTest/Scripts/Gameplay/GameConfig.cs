using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Scriptable Objects/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        public IReadOnlyList<ILevelConfig> LevelsConfig => _levelsConfig;

        [field: SerializeField, Range(0, 1)]
        public float PauseTimeScale { get; private set; } = 1f;

        [field: SerializeField]
        public Sprite HollowRatingIcon { get; private set; }

        [field: SerializeField]
        public Sprite RatingIcon { get; private set; }

        [field: SerializeField, Min(1)]
        public int MaxRating { get; private set; } = 1;

        [field: SerializeField]
        public AudioClip MenuBackgroundMusic { get; private set; }

        [SerializeField]
        private List<LevelConfig> _levelsConfig = new();

    }
}
