#nullable enable
using System;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters.Abstractions
{
    public interface IInGameMenuPresenter : IPresenter<IInGameMenuView>
    {
        public event Action? Pause;
    }
}
