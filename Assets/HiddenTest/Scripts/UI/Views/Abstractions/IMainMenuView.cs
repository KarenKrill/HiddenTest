#nullable enable

using System;

using UnityEngine;

using KarenKrill.UniCore.UI.Views.Abstractions;

namespace HiddenTest.UI.Views.Abstractions
{
    public interface IMainMenuView : IView
    {
        public event Action? NewGameRequested;
        public event Action? ExitRequested;
        public event Action? SettingsOpenRequested;
    }
}