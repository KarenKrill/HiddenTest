using System;
using UnityEngine;

namespace HiddenTest
{
    using Abstractions;
    
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Scriptable Objects/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        public bool ShowDiagnostics
        {
            get => _showDiagnostics;
            set
            {
                if (_showDiagnostics != value)
                {
                    _showDiagnostics = value;
                    ShowDiagnosticsChanged?.Invoke(value);
                }
            }
        }
        public int QualityLevel
        {
            get => _qualityLevel;
            set
            {
                if (_qualityLevel != value)
                {
                    _qualityLevel = value;
                    QualityLevelChanged?.Invoke(value);
                }
            }
        }
        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                if (_masterVolume != value)
                {
                    _masterVolume = value;
                    MasterVolumeChanged?.Invoke(value);
                }
            }
        }
        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                if (_musicVolume != value)
                {
                    _musicVolume = value;
                    MusicVolumeChanged?.Invoke(value);
                }
            }
        }
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                if (_sfxVolume != value)
                {
                    _sfxVolume = value;
                    SfxVolumeChanged?.Invoke(value);
                }
            }
        }

        public event Action<bool> ShowDiagnosticsChanged;
        public event Action<int> QualityLevelChanged;
        public event Action<float> MasterVolumeChanged;
        public event Action<float> MusicVolumeChanged;
        public event Action<float> SfxVolumeChanged;

        [SerializeField]
        private bool _showDiagnostics = true;
        [SerializeField, Min(0)]
        private int _qualityLevel = 0;
        [SerializeField, Range(0, 1)]
        private float _masterVolume = 1;
        [SerializeField, Range(0, 1)]
        private float _musicVolume = 1;
        [SerializeField, Range(0, 1)]
        private float _sfxVolume = 1;
    }
}
