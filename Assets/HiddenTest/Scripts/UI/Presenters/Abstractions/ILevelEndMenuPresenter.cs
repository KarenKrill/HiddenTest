#nullable enable
using System;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters.Abstractions
{
    public interface ILevelEndMenuPresenter : IPresenter<ILevelEndMenuView>
    {
        public event Action? Continue;
        public event Action? Restart;
        public event Action? MainMenu;
        public event Action? Exit;
    }
}
