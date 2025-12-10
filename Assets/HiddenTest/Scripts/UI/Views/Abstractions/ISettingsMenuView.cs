#nullable enable
using System;
using KarenKrill.UniCore.UI.Views.Abstractions;

namespace HiddenTest.UI.Views.Abstractions
{
    public interface ISettingsMenuView : IView
    {
        public bool ShowDiagnostics { get; set; }
        public int QualityLevel { get; set; }
        public string[] QualityLevels { set; }
        public float MasterVolume { get; set; }
        public float MusicVolume { get; set; }
        public float SfxVolume { get; set; }

        public event Action<bool>? ShowDiagnosticsChanged;
        public event Action<int>? QualityLevelChanged;
        public event Action<float>? MasterVolumeChanged;
        public event Action<float>? MusicVolumeChanged;
        public event Action<float>? SfxVolumeChanged;
        public event Action? CloseRequested;
    }
}