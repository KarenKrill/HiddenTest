#nullable enable
using System;
using UnityEngine;
using KarenKrill.UniCore.UI.Views.Abstractions;

namespace HiddenTest.UI.Views.Abstractions
{
    public interface IInGameMenuView : IView
    {
        public string RemainingTimeText { set; }

        public event Action? PauseRequested;

        public void SetRatingIcon(int index, Sprite sprite);
        public void AddTask(string name, Sprite icon, TaskViewType taskViewType);
        public void RemoveTask(string name);
        public void ClearTaskList();
    }
}