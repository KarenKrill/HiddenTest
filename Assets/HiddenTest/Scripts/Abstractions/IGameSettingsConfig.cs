#nullable enable
using System;

namespace HiddenTest.Abstractions
{
    public interface IGameSettingsConfig
    {
        public bool ShowDiagnostics { get; set; }
        public int QualityLevel { get; set; }
        public float MasterVolume { get; set; }
        public float MusicVolume { get; set; }
        public float SfxVolume { get; set; }

        public event Action<bool>? ShowDiagnosticsChanged;
        public event Action<int>? QualityLevelChanged;
        public event Action<float>? MasterVolumeChanged;
        public event Action<float>? MusicVolumeChanged;
        public event Action<float>? SfxVolumeChanged;
    }
}
