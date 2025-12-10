#nullable enable

using System;

using KarenKrill.UniCore.UI.Presenters.Abstractions;

using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters.Abstractions
{
    public interface ISettingsMenuPresenter : IPresenter<ISettingsMenuView>
    {
        public event Action? Close;
    }
}
