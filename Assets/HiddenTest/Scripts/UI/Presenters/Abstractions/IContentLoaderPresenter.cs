#nullable enable

using System;

using KarenKrill.UniCore.UI.Presenters.Abstractions;

using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters.Abstractions
{
    public interface IContentLoaderPresenter : IPresenter<IContentLoaderView>
    {
        public string StageText { set; }
        public float ProgressValue { set; }
        public string StatusText { set; }
        public bool EnableContinue { set; }

        public event Action? Continue;
    }
}
