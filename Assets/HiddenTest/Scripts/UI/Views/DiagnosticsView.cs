using UnityEngine;
using TMPro;
using KarenKrill.UniCore.UI.Views;

namespace HiddenTest.UI.Views
{
    using Abstractions;
    
    public class DiagnosticsView : ViewBehaviour, IDiagnosticsView
    {
        public string FpsText { set => _fpsText.text = value; }

        [SerializeField]
        private TextMeshProUGUI _fpsText;
    }
}