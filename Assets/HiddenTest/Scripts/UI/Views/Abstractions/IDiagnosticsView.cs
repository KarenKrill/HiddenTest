using KarenKrill.UniCore.UI.Views.Abstractions;

namespace HiddenTest.UI.Views.Abstractions
{
    public interface IDiagnosticsView : IView
    {
        public string FpsText { set; }
    }
}