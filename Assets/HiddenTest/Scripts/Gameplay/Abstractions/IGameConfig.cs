using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IGameConfig
    {
        public IReadOnlyList<ILevelConfig> LevelsConfig { get; }
        public float PauseTimeScale { get; }
        public Sprite HollowRatingIcon { get; }
        public Sprite RatingIcon { get; }
        public int MaxRating { get; }
        public AudioClip MenuBackgroundMusic { get; }
    }
}
