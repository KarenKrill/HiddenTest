using System;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.UniCore.UI.Views.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters
{
    using Abstractions;

    public class InGameMenuPresenter : PresenterBase<IInGameMenuView>, IInGameMenuPresenter, IPresenter<IInGameMenuView>
    {
        public event Action Pause;

        public InGameMenuPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator) : base(viewFactory, navigator)
        {
        }

        protected override void Subscribe()
        {
            View.PauseRequested += OnPause;
        }

        protected override void Unsubscribe()
        {
            View.PauseRequested -= OnPause;
        }

        private void OnPause() => Pause?.Invoke();
    }
}
