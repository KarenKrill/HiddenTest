#nullable enable
using UnityEngine;
using KarenKrill.UniCore.StateSystem.Abstractions;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.Audio.Abstractions;
using HiddenTest.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class InitialStateHandler : IStateHandler<GameState>
    {
        public GameState State => GameState.Initial;

        public InitialStateHandler(ILogger logger,
            IGameStateNavigator gameFlow,
            IGameConfig gameConfig,
            IAudioController audioController,
            IPresenter<IDiagnosticsView> diagnosticsPresenter)
        {
            _logger = logger;
            _gameFlow = gameFlow;
            _gameConfig = gameConfig;
            _audioController = audioController;
            _diagnosticsPresenter = diagnosticsPresenter;
        }

        public void Enter(GameState prevState, object? context = null)
        {
            _logger.Log(nameof(InitialStateHandler), nameof(Enter));
            _gameConfig.ShowDiagnosticsChanged += OnShowDiagnosticsChanged;
            _gameConfig.MasterVolumeChanged += OnMasterVolumeChanged;
            _gameConfig.MusicVolumeChanged += OnMusicVolumeChanged;
            _gameConfig.SfxVolumeChanged += OnSfxVolumeChanged;
            _gameConfig.QualityLevelChanged += OnQualityLevelChanged;
            if (_gameConfig.ShowDiagnostics)
            {
                _diagnosticsPresenter.Enable();
            }
            _audioController.MasterVolume = _gameConfig.MasterVolume;
            _audioController.MusicVolume = _gameConfig.MusicVolume;
            _audioController.SfxVolume = _gameConfig.SfxVolume;
            _gameFlow.LoadMainMenu();
        }

        public void Exit(GameState nextState)
        {
            _logger.Log(nameof(InitialStateHandler), nameof(Exit));
        }

        private readonly ILogger _logger;
        private readonly IGameStateNavigator _gameFlow;
        private readonly IGameConfig _gameConfig;
        private readonly IAudioController _audioController;
        private readonly IPresenter<IDiagnosticsView> _diagnosticsPresenter;

        private void OnShowDiagnosticsChanged(bool state)
        {
            if (state)
            {
                _diagnosticsPresenter.Enable();
            }
            else
            {
                _diagnosticsPresenter.Disable();
            }
        }

        private void OnQualityLevelChanged(int qualityLevel)
        {
            QualitySettings.SetQualityLevel(qualityLevel);
        }

        private void OnMasterVolumeChanged(float value)
        {
            _audioController.MasterVolume = value;
        }
        private void OnMusicVolumeChanged(float value)
        {
            _audioController.MusicVolume = value;
        }
        private void OnSfxVolumeChanged(float value)
        {
            _audioController.SfxVolume = value;
        }
    }
}
