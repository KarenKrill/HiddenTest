using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KarenKrill.UniCore.UI.Views;

namespace HiddenTest.UI.Views
{
    using Abstractions;

    public class LevelEndMenuView : ViewBehaviour, ILevelEndMenuView
    {
        public string TitleText { set => _titleText.text = value; }
        public Color TitleTextColor { set => _titleText.color = value; }
        public bool EnableContinue { set => _continueButton.interactable = value; }

        public event Action ContinueRequested;
        public event Action RestartRequested;
        public event Action MainMenuExitRequested;
        public event Action ExitRequested;

        public void SetRatingIcon(int index, Sprite sprite)
        {
            _ratingImages[index].sprite = sprite;
        }

        [SerializeField]
        private List<Image> _ratingImages = new();
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _mainMenuExitButton;
        [SerializeField]
        private Button _exitButton;
        [SerializeField]
        private TextMeshProUGUI _titleText;

        private void Awake()
        {
            _continueButton.interactable = false;
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuExitButton.onClick.AddListener(OnMainMenuExitButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _mainMenuExitButton.onClick.RemoveListener(OnMainMenuExitButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnContinueButtonClicked() => ContinueRequested?.Invoke();

        private void OnRestartButtonClicked() => RestartRequested?.Invoke();

        private void OnMainMenuExitButtonClicked() => MainMenuExitRequested?.Invoke();

        private void OnExitButtonClicked() => ExitRequested?.Invoke();
    }
}