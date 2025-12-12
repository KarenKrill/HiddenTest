#nullable enable

using System;

using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.UniCore.UI.Views.Abstractions;

using HiddenTest.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters
{
    using Abstractions;
    using UnityEngine;

    public class SettingsMenuPresenter : PresenterBase<ISettingsMenuView>, ISettingsMenuPresenter, IPresenter<ISettingsMenuView>
    {
        public event Action? Close;

        public SettingsMenuPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator,
            IGameSettingsConfig gameSettingsConfig) : base(viewFactory, navigator)
        {
            _gameSettingsConfig = gameSettingsConfig;
        }

        protected override void Subscribe()
        {
            _gameSettingsConfig.ShowDiagnosticsChanged += OnModelShowDiagnosticsChanged;
            _gameSettingsConfig.QualityLevelChanged += OnModelQualityLevelChanged;
            _gameSettingsConfig.MasterVolumeChanged += OnModelMasterVolumeChanged;
            _gameSettingsConfig.MusicVolumeChanged += OnModelMusicVolumeChanged;
            _gameSettingsConfig.SfxVolumeChanged += OnModelSfxVolumeChanged;

            View.ShowDiagnostics = _gameSettingsConfig.ShowDiagnostics;
            View.QualityLevels = QualitySettings.names;
            View.QualityLevel = _gameSettingsConfig.QualityLevel;
            View.MasterVolume = _gameSettingsConfig.MasterVolume;
            View.MusicVolume = _gameSettingsConfig.MusicVolume;
            View.SfxVolume = _gameSettingsConfig.SfxVolume;

            View.ShowDiagnosticsChanged += OnViewShowDiagnosticsChanged;
            View.QualityLevelChanged += OnViewQualityLevelChanged;
            View.MasterVolumeChanged += OnViewMasterVolumeChanged;
            View.MusicVolumeChanged += OnViewMusicVolumeChanged;
            View.SfxVolumeChanged += OnViewSfxVolumeChanged;
            View.CloseRequested += OnClose;
        }
        protected override void Unsubscribe()
        {
            _gameSettingsConfig.ShowDiagnosticsChanged -= OnModelShowDiagnosticsChanged;
            _gameSettingsConfig.QualityLevelChanged -= OnModelQualityLevelChanged;
            _gameSettingsConfig.MasterVolumeChanged -= OnModelMasterVolumeChanged;
            _gameSettingsConfig.MusicVolumeChanged -= OnModelMusicVolumeChanged;
            _gameSettingsConfig.SfxVolumeChanged -= OnModelSfxVolumeChanged;

            View.ShowDiagnosticsChanged -= OnViewShowDiagnosticsChanged;
            View.QualityLevelChanged -= OnViewQualityLevelChanged;
            View.MasterVolumeChanged -= OnViewMasterVolumeChanged;
            View.MusicVolumeChanged -= OnViewMusicVolumeChanged;
            View.SfxVolumeChanged -= OnViewSfxVolumeChanged;
            View.CloseRequested -= OnClose;
        }

        private readonly IGameSettingsConfig _gameSettingsConfig;
        private bool _isViewChange = false;

        private void OnModelShowDiagnosticsChanged(bool state)
        {
            if (!_isViewChange)
            {
                View.ShowDiagnostics = state;
            }
        }
        private void OnModelQualityLevelChanged(int qualityLevel)
        {
            if (!_isViewChange)
            {
                View.QualityLevel = qualityLevel;
            }
        }
        private void OnModelMasterVolumeChanged(float volume)
        {
            if (!_isViewChange)
            {
                View.MasterVolume = volume;
            }
        }
        private void OnModelMusicVolumeChanged(float volume)
        {
            if (!_isViewChange)
            {
                View.MusicVolume = volume;
            }
        }
        private void OnModelSfxVolumeChanged(float volume)
        {
            if (!_isViewChange)
            {
                View.SfxVolume = volume;
            }
        }

        private void OnViewShowDiagnosticsChanged(bool state)
        {
            _isViewChange = true;
            _gameSettingsConfig.ShowDiagnostics = state;
            _isViewChange = false;
        }
        private void OnViewQualityLevelChanged(int qualityLevel)
        {
            _isViewChange = true;
            _gameSettingsConfig.QualityLevel = qualityLevel;
            _isViewChange = false;
        }
        private void OnViewMasterVolumeChanged(float volume)
        {
            _isViewChange = true;
            _gameSettingsConfig.MasterVolume = volume;
            _isViewChange = false;
        }
        private void OnViewMusicVolumeChanged(float volume)
        {
            _isViewChange = true;
            _gameSettingsConfig.MusicVolume = volume;
            _isViewChange = false;
        }
        private void OnViewSfxVolumeChanged(float volume)
        {
            _isViewChange = true;
            _gameSettingsConfig.SfxVolume = volume;
            _isViewChange = false;
        }

        private void OnClose() => Close?.Invoke();
    }
}