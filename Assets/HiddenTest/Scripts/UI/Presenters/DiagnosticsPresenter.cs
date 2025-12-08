using KarenKrill.UniCore.Diagnostics.Abstractions;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.UniCore.UI.Views.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters
{
    public class DiagnosticsPresenter : PresenterBase<IDiagnosticsView>, IPresenter<IDiagnosticsView>
    {
        public DiagnosticsPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator,
            IDiagnosticsProvider diagnosticsProvider) : base(viewFactory, navigator)
        {
            _diagnosticsProvider = diagnosticsProvider;
        }

        protected override void Subscribe()
        {
            OnPerfomanceInfoChanged(_diagnosticsProvider.PerfomanceInfo);
            _diagnosticsProvider.PerfomanceInfoChanged += OnPerfomanceInfoChanged;
        }

        protected override void Unsubscribe()
        {
            _diagnosticsProvider.PerfomanceInfoChanged -= OnPerfomanceInfoChanged;
        }

        private readonly IDiagnosticsProvider _diagnosticsProvider;

        private void OnPerfomanceInfoChanged(PerfomanceInfo perfomanceInfo)
        {
            View.FpsText = $"Fps: {perfomanceInfo.FpsAverage:0.0}";
        }
    }
}