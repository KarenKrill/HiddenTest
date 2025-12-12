#nullable enable
using System;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface ILevelSession
    {
        public int ActiveLevel { get; set; }
        public float RemainingTime { get; set; }
        public int Rating { get;  set; }

        public event Action<int>? ActiveLevelChanged;
        public event Action<float>? RemainingTimeChanged;
        public event Action<int>? RatingChanged;
    }
}
