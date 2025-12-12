#nullable enable
using UnityEngine;
using KarenKrill.UniCore.UI.Views.Abstractions;

namespace HiddenTest.UI.Views.Abstractions
{
    public enum TaskViewType
    {
        NameOnly,
        IconOnly,
        NameAndIcon
    }

    public interface ITaskView : IView
    {
        public string Name { set; }
        public Sprite Icon { set; }
        public TaskViewType Type { set; }
    }
}