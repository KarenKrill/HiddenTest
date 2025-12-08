#nullable enable
using System;
using UnityEngine;
using KarenKrill.UniCore.UI.Views.Abstractions;

namespace HiddenTest.UI.Views.Abstractions
{
    public interface ILevelEndMenuView : IView
    {
        public string TitleText { set; }
        public Color TitleTextColor { set; }
        public bool EnableContinue { set; }

        public event Action? ContinueRequested;
        public event Action? RestartRequested;
        public event Action? MainMenuExitRequested;
        public event Action? ExitRequested;

        public void SetRatingIcon(int index, Sprite sprite);
    }
}