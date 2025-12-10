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
            IGameConfig gameConfig) : base(viewFactory, navigator)
        {
            _gameConfig = gameConfig;
        }

        protected override void Subscribe()
        {
            _gameConfig.ShowDiagnosticsChanged += OnModelShowDiagnosticsChanged;
            _gameConfig.QualityLevelChanged += OnModelQualityLevelChanged;
            _gameConfig.MasterVolumeChanged += OnModelMasterVolumeChanged;
            _gameConfig.MusicVolumeChanged += OnModelMusicVolumeChanged;
            _gameConfig.SfxVolumeChanged += OnModelSfxVolumeChanged;

            View.ShowDiagnostics = _gameConfig.ShowDiagnostics;
            View.QualityLevels = QualitySettings.names;
            View.QualityLevel = _gameConfig.QualityLevel;
            View.MasterVolume = _gameConfig.MasterVolume;
            View.MusicVolume = _gameConfig.MusicVolume;
            View.SfxVolume = _gameConfig.SfxVolume;

            View.ShowDiagnosticsChanged += OnViewShowDiagnosticsChanged;
            View.QualityLevelChanged += OnViewQualityLevelChanged;
            View.MasterVolumeChanged += OnViewMasterVolumeChanged;
            View.MusicVolumeChanged += OnViewMusicVolumeChanged;
            View.SfxVolumeChanged += OnViewSfxVolumeChanged;
            View.CloseRequested += OnClose;
        }
        protected override void Unsubscribe()
        {
            _gameConfig.ShowDiagnosticsChanged -= OnModelShowDiagnosticsChanged;
            _gameConfig.QualityLevelChanged -= OnModelQualityLevelChanged;
            _gameConfig.MasterVolumeChanged -= OnModelMasterVolumeChanged;
            _gameConfig.MusicVolumeChanged -= OnModelMusicVolumeChanged;
            _gameConfig.SfxVolumeChanged -= OnModelSfxVolumeChanged;

            View.ShowDiagnosticsChanged -= OnViewShowDiagnosticsChanged;
            View.QualityLevelChanged -= OnViewQualityLevelChanged;
            View.MasterVolumeChanged -= OnViewMasterVolumeChanged;
            View.MusicVolumeChanged -= OnViewMusicVolumeChanged;
            View.SfxVolumeChanged -= OnViewSfxVolumeChanged;
            View.CloseRequested -= OnClose;
        }

        private readonly IGameConfig _gameConfig;
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
            _gameConfig.ShowDiagnostics = state;
            _isViewChange = false;
        }
        private void OnViewQualityLevelChanged(int qualityLevel)
        {
            _isViewChange = true;
            _gameConfig.QualityLevel = qualityLevel;
            _isViewChange = false;
        }
        private void OnViewMasterVolumeChanged(float volume)
        {
            _isViewChange = true;
            _gameConfig.MasterVolume = volume;
            _isViewChange = false;
        }
        private void OnViewMusicVolumeChanged(float volume)
        {
            _isViewChange = true;
            _gameConfig.MusicVolume = volume;
            _isViewChange = false;
        }
        private void OnViewSfxVolumeChanged(float volume)
        {
            _isViewChange = true;
            _gameConfig.SfxVolume = volume;
            _isViewChange = false;
        }

        private void OnClose() => Close?.Invoke();
    }
}