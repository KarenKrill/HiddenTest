using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using KarenKrill.UniCore.Instantiattion;
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
            if (index < _ratingImages.Count)
            {
                _ratingImages[index].sprite = sprite;
            }
            else
            {
                throw new NotSupportedException($"{nameof(InGameMenuView)} doesn't support rating more than {_ratingImages.Count}");
            }
        }
        public void AddTask(string name, Sprite icon, TaskViewType taskViewType)
        {
            var taskView = _taskViewsPool.Get();
            taskView.Name = name;
            taskView.Icon = icon;
            taskView.Type = taskViewType;
            _taskViews[name] = taskView;
            taskView.ShowSmoothlyAsync().AsUniTask().Forget();
        }
        public void RemoveTask(string name)
        {
            if (_taskViews.TryGetValue(name, out var taskView))
            {
                _taskViews.Remove(name);
                taskView.CloseSmoothlyAsync().AsUniTask().ContinueWith(() =>
                {
                    _taskViewsPool.Release(taskView);
                }).Forget();
            }
        }
        public void ClearTaskList()
        {
            foreach (var taskView in _taskViews.Values)
            {
                taskView.Close(false);
                _taskViewsPool.Release(taskView);
            }
            _taskViews.Clear();
            _taskViewsPool.Clear();
        }

        [SerializeField]
        private List<Image> _ratingImages = new();
        [SerializeField]
        private TextMeshProUGUI _remainingTimeText;
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private Transform _taskViewsParent;
        [SerializeField]
        private TaskView _taskViewPrefab;
        [SerializeField]
        private int _defaultPoolCapacity = 6;

        private ComponentPool<TaskView> _taskViewsPool;
        private readonly Dictionary<string, TaskView> _taskViews = new();

        private void Awake()
        {
            _taskViewsPool = new(_taskViewPrefab, _taskViewsParent, _defaultPoolCapacity);
        }
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