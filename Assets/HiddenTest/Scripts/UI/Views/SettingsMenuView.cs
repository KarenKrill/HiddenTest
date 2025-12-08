using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KarenKrill.UniCore.UI.Views;

namespace HiddenTest.UI.Views
{
    using Abstractions;

    public class SettingsMenuView : ViewBehaviour, ISettingsMenuView
    {
        public bool ShowDiagnostics { get => _showDiagnosticsToggle.isOn; set => _showDiagnosticsToggle.isOn = value; }

        public int QualityLevel { get => _qualityLevelDropdown.value; set => _qualityLevelDropdown.value = value; }
        public string[] QualityLevels
        {
            set
            {
                if (_qualityLevels != value)
                {
                    _qualityLevels = value;
                    _qualityLevelDropdown.options.Clear();
                    foreach (var level in value)
                    {
                        _qualityLevelDropdown.options.Add(new TMP_Dropdown.OptionData(level));
                    }
                }
            }
        }

        public float MasterVolume { get => _masterVolumeSlider.normalizedValue; set => _masterVolumeSlider.normalizedValue = value; }
        public float MusicVolume { get => _musicVolumeSlider.normalizedValue; set => _musicVolumeSlider.normalizedValue = value; }
        public float SfxVolume { get => _sfxVolumeSlider.normalizedValue; set => _sfxVolumeSlider.normalizedValue = value; }

        public event Action<bool> ShowDiagnosticsChanged;
        public event Action<int> QualityLevelChanged;
        public event Action<float> MasterVolumeChanged;
        public event Action<float> MusicVolumeChanged;
        public event Action<float> SfxVolumeChanged;
        public event Action CloseRequested;

        [SerializeField]
        private Toggle _showDiagnosticsToggle;
        [SerializeField]
        private TMP_Dropdown _qualityLevelDropdown;
        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private Slider _musicVolumeSlider;
        [SerializeField]
        private Slider _sfxVolumeSlider;
        [SerializeField]
        private Button _closeButton;

        private string[] _qualityLevels;

        private void OnEnable()
        {
            _showDiagnosticsToggle.onValueChanged.AddListener(OnShowDiagnosticsToggleValueChanged);
            _qualityLevelDropdown.onValueChanged.AddListener(OnQualityLevelDropdownValueChanged);
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderValueChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChanged);
            _sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeSliderValueChanged);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
        private void OnDisable()
        {
            _showDiagnosticsToggle.onValueChanged.RemoveListener(OnShowDiagnosticsToggleValueChanged);
            _qualityLevelDropdown.onValueChanged.RemoveListener(OnQualityLevelDropdownValueChanged);
            _masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeSliderValueChanged);
            _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderValueChanged);
            _sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeSliderValueChanged);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnQualityLevelDropdownValueChanged(int level) => QualityLevelChanged?.Invoke(level);
        private void OnShowDiagnosticsToggleValueChanged(bool state) => ShowDiagnosticsChanged?.Invoke(state);
        private void OnMasterVolumeSliderValueChanged(float value) => MasterVolumeChanged?.Invoke(value);
        private void OnMusicVolumeSliderValueChanged(float value) => MusicVolumeChanged?.Invoke(value);
        private void OnSfxVolumeSliderValueChanged(float value) => SfxVolumeChanged?.Invoke(value);
        private void OnCloseButtonClicked() => CloseRequested?.Invoke();
    }
}
