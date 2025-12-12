#nullable enable

using System;

using KarenKrill.UniCore.UI.Presenters.Abstractions;

using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters.Abstractions
{
    public interface IMainMenuPresenter : IPresenter<IMainMenuView>
    {
        public event Action? NewGame;
        public event Action? Exit;
    }
}
