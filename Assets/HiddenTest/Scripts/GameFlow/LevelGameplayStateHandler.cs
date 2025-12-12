#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using KarenKrill.UniCore.Input.Abstractions;
using KarenKrill.UniCore.StateSystem.Abstractions;
using KarenKrill.Audio.Abstractions;
using KarenKrill.Utilities;
using HiddenTest.Gameplay.Abstractions;
using HiddenTest.Input.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class LevelGameplayStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.LevelGameplay;

        public LevelGameplayStateHandler(IGameStateNavigator gameStateNavigator,
            IBasicActionsProvider actionsProvider,
            IPlayerActionsProvider playerActionsProvider,
            IInGameMenuPresenter inGameMenuPresenter,
            IGameConfig gameConfig,
            ILevelItemsRegistry levelItemsRegistry,
            ILevelSession levelSession,
            IGameTaskController<IHiddenObject> taskController,
            IAudioController audioController) : base(inGameMenuPresenter)
        {
            _gameStateNavigator = gameStateNavigator;
            _actionsProvider = actionsProvider;
            _playerActionsProvider = playerActionsProvider;
            _inGameMenuPresenter = inGameMenuPresenter;
            _gameConfig = gameConfig;
            _levelItemsRegistry = levelItemsRegistry;
            _taskController = taskController;
            _levelSession = levelSession;
            _audioController = audioController;
            _levelItemsRegistry.ItemRegistered += OnLevelItemRegistered;
            _levelItemsRegistry.ItemUnregistered += OnLevelItemUnregistered;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
            if (_levelSession.ActiveLevel >= 0)
            {
                _levelConfig = _gameConfig.LevelsConfig[_levelSession.ActiveLevel];
                UpdateTasks();
                 
                if(prevState != GameState.Pause)
                {
                    if (_levelConfig.BackgroundMusic != null)
                    {
                        _audioController.PlayMusic(_levelConfig.BackgroundMusic);
                    }
                    if (_levelConfig.TimeLimitEnabled)
                    {
                        var cts = CancellationTokenSource.CreateLinkedTokenSource(Application.exitCancellationToken);
                        _levelTimerCts = cts;
                        TimerUtility.StartCountdownAsync(_levelConfig.TimeLimitInSeconds, 0.1f, OnLevelTimerTick, OnLevelTimerCompleted, cts.Token)
                            .ContinueWith(() =>
                            {
                                cts.Dispose();
                                _levelTimerCts = null;
                            })
                            .Forget();
                    }
                }
            }
            _playerActionsProvider.Pause += OnPause;
            _inGameMenuPresenter.Pause += OnPause;
            _actionsProvider.SetActionMap(ActionMap.Player);
        }

        public override void Exit(GameState nextState)
        {
            _playerActionsProvider.Pause -= OnPause;
            _inGameMenuPresenter.Pause -= OnPause;
            if (nextState != GameState.Pause)
            {
                _taskController.ClearTasks();
                _hiddenObjects.Clear();
                _levelTimerCts?.Cancel();
                _levelTimerCts = null;
                _audioController.StopMusic();
            }
            else
            {
                _gameStateNavigator.StateChanging += OnPauseStateChanging;
            }
            base.Exit(nextState);
        }

        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly IBasicActionsProvider _actionsProvider;
        private readonly IPlayerActionsProvider _playerActionsProvider;
        private readonly IInGameMenuPresenter _inGameMenuPresenter;
        private readonly IGameConfig _gameConfig;
        private readonly ILevelItemsRegistry _levelItemsRegistry;
        private readonly ILevelSession _levelSession;
        private readonly IGameTaskController<IHiddenObject> _taskController;
        private readonly IAudioController _audioController;
        private ILevelConfig? _levelConfig;
        private CancellationTokenSource? _levelTimerCts;
        private readonly List<IHiddenObject> _hiddenObjects = new();

        private void UpdateTasks()
        {
            for (int i = 0; i < _hiddenObjects.Count
                && _taskController.TaskList.Count < _levelConfig?.MaxTaskCount; i++)
            {
                var hiddenObject = _hiddenObjects[i];
                var task = _taskController.TaskList.FirstOrDefault(task => task.Context.Config.Id == hiddenObject.Config.Id);
                if (task == null)
                {
                    _taskController.AddTask(hiddenObject.Config.Name, hiddenObject);
                }
            }
        }

        private void OnLevelItemRegistered(int id, IHiddenObject hiddenObject)
        {
            hiddenObject.Clicked += OnHiddenObjectClicked;
            _hiddenObjects.Add(hiddenObject);
        }
        private void OnLevelItemUnregistered(int id, IHiddenObject hiddenObject)
        {
            hiddenObject.Clicked -= OnHiddenObjectClicked;
            if (_hiddenObjects.Remove(hiddenObject))
            {
                var task = _taskController.TaskList.FirstOrDefault(task => task.Context.Config.Id == hiddenObject.Config.Id);
                if (task != null)
                {
                    _taskController.RemoveTask(task);
                }
            }
        }
        private void OnHiddenObjectClicked(IHiddenObject hiddenObject)
        {
            var task = _taskController.TaskList.FirstOrDefault(task => task.Context == hiddenObject);
            if (task != null)
            {
                if (_hiddenObjects.Remove(hiddenObject))
                {
                    _taskController.RemoveTask(task);
                    UpdateTasks();
                    Debug.Log($"{hiddenObject.Config.Name} clicked");
                    if (_taskController.TaskList.Count == 0)
                    {
                        _gameStateNavigator.FinishLevel();
                    }
                }
            }
        }
        private void OnPause()
        {
            _gameStateNavigator.PauseLevel();
        }
        private void OnPauseStateChanging(GameState fromState, GameState toState)
        {
            _gameStateNavigator.StateChanging -= OnPauseStateChanging;
            if (toState != GameState.LevelGameplay)
            {
                base.Enter(GameState.LevelGameplay);
                Exit(toState);
            }
        }
        private void OnLevelTimerTick(float leftTime)
        {
            _levelSession.RemainingTime = leftTime;
            _levelSession.Rating = Mathf.FloorToInt((3 * leftTime / _levelConfig?.TimeLimitInSeconds) + 1 ?? 0);
        }
        private void OnLevelTimerCompleted()
        {
            _levelSession.RemainingTime = 0;
            _levelSession.Rating = 0;
            _gameStateNavigator.FinishLevel();
        }
    }
}
