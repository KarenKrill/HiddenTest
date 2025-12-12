using System;
using System.Globalization;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.UniCore.UI.Views.Abstractions;
using HiddenTest.Gameplay.Abstractions;
using HiddenTest.UI.Views.Abstractions;

namespace HiddenTest.UI.Presenters
{
    using Abstractions;

    public class InGameMenuPresenter : PresenterBase<IInGameMenuView>, IInGameMenuPresenter, IPresenter<IInGameMenuView>
    {
        public event Action Pause;

        public InGameMenuPresenter(ILevelSession levelSession,
            IGameConfig gameConfig,
            IViewFactory viewFactory,
            IPresenterNavigator navigator,
            IGameTaskController<IHiddenObject> taskController) : base(viewFactory, navigator)
        {
            _levelSession = levelSession;
            _gameConfig = gameConfig;
            _taskController = taskController;
        }

        protected override void Subscribe()
        {
            View.PauseRequested += OnPause;
            _levelSession.ActiveLevelChanged += OnActiveLevelChanged;
            _levelSession.RemainingTimeChanged += OnRemainingTimeChanged;
            _levelSession.RatingChanged += OnRatingChanged;
            _taskController.TaskAdded += OnTaskAdded;
            _taskController.TaskRemoved += OnTaskRemoved;
            _taskController.TaskCleared += OnTaskCleared;
            OnActiveLevelChanged(_levelSession.ActiveLevel);
            OnRemainingTimeChanged(_levelSession.RemainingTime);
            OnRatingChanged(_levelSession.Rating);
        }

        protected override void Unsubscribe()
        {
            View.PauseRequested -= OnPause;
            _levelSession.ActiveLevelChanged -= OnActiveLevelChanged;
            _levelSession.RemainingTimeChanged -= OnRemainingTimeChanged;
            _levelSession.RatingChanged -= OnRatingChanged;
            _taskController.TaskAdded -= OnTaskAdded;
            _taskController.TaskRemoved -= OnTaskRemoved;
            _taskController.TaskCleared -= OnTaskCleared;
        }

        private readonly ILevelSession _levelSession;
        private readonly IGameConfig _gameConfig;
        private readonly IGameTaskController<IHiddenObject> _taskController;
        private Views.Abstractions.TaskViewType _taskViewType = Views.Abstractions.TaskViewType.NameOnly;

        private void OnPause() => Pause?.Invoke();

        private void OnActiveLevelChanged(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < _gameConfig.LevelsConfig.Count)
            {
                _taskViewType = _gameConfig.LevelsConfig[levelIndex].TaskViewType switch
                {
                    Gameplay.Abstractions.TaskViewType.NameOnly => Views.Abstractions.TaskViewType.NameOnly,
                    Gameplay.Abstractions.TaskViewType.IconOnly => Views.Abstractions.TaskViewType.IconOnly,
                    _ => Views.Abstractions.TaskViewType.NameAndIcon,
                };
            }
        }

        private void OnRemainingTimeChanged(float leftTime)
        {
            View.RemainingTimeText = $"{leftTime.ToString("00.00", CultureInfo.InvariantCulture)}";
        }

        private void OnRatingChanged(int rating)
        {
            for (int i = 0; i < _gameConfig.MaxRating; i++)
            {
                View.SetRatingIcon(i, i >= rating ? _gameConfig.HollowRatingIcon : _gameConfig.RatingIcon);
            }
        }

        private void OnTaskAdded(IGameTask<IHiddenObject> gameTask)
        {
            View.AddTask(gameTask.Context.Config.Name, gameTask.Context.Config.Icon, _taskViewType);
        }

        private void OnTaskRemoved(IGameTask<IHiddenObject> gameTask)
        {
            View.RemoveTask(gameTask.Context.Config.Name);
        }

        private void OnTaskCleared()
        {
            View.ClearTaskList();
        }
    }
}
