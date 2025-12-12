using System;
using UnityEngine;

namespace HiddenTest.Gameplay
{
    using Abstractions;
    
    public class LevelSession : ILevelSession
    {
        public int ActiveLevel
        {
            get => _activeLevel;
            set
            {
                if (_activeLevel != value)
                {
                    _activeLevel = value;
                    ActiveLevelChanged?.Invoke(value);
                }
            }
        }
        public float RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (Mathf.Abs(_remainingTime - value) > float.Epsilon)
                {
                    _remainingTime = value;
                    RemainingTimeChanged?.Invoke(value);
                }
            }
        }
        public int Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    RatingChanged?.Invoke(value);
                }
            }
        }

        public event Action<int> ActiveLevelChanged;
        public event Action<float> RemainingTimeChanged;
        public event Action<int> RatingChanged;

        private int _activeLevel = -1;
        private float _remainingTime = 0;
        private int _rating = 0;
    }
}
