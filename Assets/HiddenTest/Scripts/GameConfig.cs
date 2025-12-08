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
            get => _showDiagnosticsInternal;
            set
            {
                if (_showDiagnosticsInternal != value)
                {
                    _showDiagnosticsInternal = value;
                    ShowDiagnosticsChanged?.Invoke(value);
                }
            }
        }
        public int QualityLevel
        {
            get => _qualityLevelInternal;
            set
            {
                if (_qualityLevelInternal != value)
                {
                    _qualityLevelInternal = value;
                    QualityLevelChanged?.Invoke(value);
                }
            }
        }
        public float MasterVolume
        {
            get => _masterVolumeInternal;
            set
            {
                if (_masterVolumeInternal != value)
                {
                    _masterVolumeInternal = value;
                    MasterVolumeChanged?.Invoke(value);
                }
            }
        }
        public float MusicVolume
        {
            get => _musicVolumeInternal;
            set
            {
                if (_musicVolumeInternal != value)
                {
                    _musicVolumeInternal = value;
                    MusicVolumeChanged?.Invoke(value);
                }
            }
        }
        public float SfxVolume
        {
            get => _sfxVolumeInternal;
            set
            {
                if (_sfxVolumeInternal != value)
                {
                    _sfxVolumeInternal = value;
                    SfxVolumeChanged?.Invoke(value);
                }
            }
        }

        public event Action<bool> ShowDiagnosticsChanged;
        public event Action<int> QualityLevelChanged;
        public event Action<float> MasterVolumeChanged;
        public event Action<float> MusicVolumeChanged;
        public event Action<float> SfxVolumeChanged;

        [SerializeField, HideInInspector]
        private bool _showDiagnosticsInternal = true;
        [SerializeField, HideInInspector, Min(0)]
        private int _qualityLevelInternal = 0;
        [SerializeField, HideInInspector, Range(0, 1)]
        private float _masterVolumeInternal = 1;
        [SerializeField, HideInInspector, Range(0, 1)]
        private float _musicVolumeInternal = 1;
        [SerializeField, HideInInspector, Range(0, 1)]
        private float _sfxVolumeInternal = 1;

#if UNITY_EDITOR
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

        private void OnValidate()
        {
            ShowDiagnostics = _showDiagnostics;
            QualityLevel = _qualityLevel;
            MasterVolume = _masterVolume;
            MusicVolume = _musicVolume;
            SfxVolume = _sfxVolume;
        }
#endif
    }
}
