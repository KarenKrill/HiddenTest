using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KarenKrill.UniCore.UI.Views;

namespace HiddenTest.UI.Views
{
    using Abstractions;

    public class InGameMenuView : ViewBehaviour, IInGameMenuView
    {
        public string RemainingTimeText { set => _remainingTimeText.text = value; }

        public event Action PauseRequested;

        public void SetRatingIcon(int index, Sprite sprite)
        {
            _ratingImages[index].sprite = sprite;
        }

        [SerializeField]
        private List<Image> _ratingImages = new();
        [SerializeField]
        private TextMeshProUGUI _remainingTimeText;
        [SerializeField]
        private Button _pauseButton;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }

        private void OnPauseButtonClicked() => PauseRequested?.Invoke();
    }
}