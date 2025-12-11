#nullable enable
using System.Linq;
using UnityEngine;
using KarenKrill.UniCore.Input.Abstractions;
using KarenKrill.UniCore.StateSystem.Abstractions;
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
            ILevelItemsRegistry levelItemsRegistry) : base(inGameMenuPresenter)
        {
            _gameStateNavigator = gameStateNavigator;
            _actionsProvider = actionsProvider;
            _playerActionsProvider = playerActionsProvider;
            _inGameMenuPresenter = inGameMenuPresenter;
            _gameConfig = gameConfig;
            _levelItemsRegistry = levelItemsRegistry;
            _levelItemsRegistry.ItemRegistered += OnLevelItemRegistered;
            _levelItemsRegistry.ItemUnregistered += OnLevelItemUnregistered;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            base.Enter(prevState);
            if (context is LevelLoadContext levelLoadContext && levelLoadContext.LevelIndex >= 0)
            {
                _gameConfig.ActiveLevel = levelLoadContext.LevelIndex;
                _levelConfig = _gameConfig.LevelsConfig[levelLoadContext.LevelIndex];
            }
            _playerActionsProvider.Pause += OnPause;
            _inGameMenuPresenter.Pause += OnPause;
            _actionsProvider.SetActionMap(ActionMap.Player);
        }

        public override void Exit(GameState nextState)
        {
            base.Exit(nextState);
            _playerActionsProvider.Pause -= OnPause;
            _inGameMenuPresenter.Pause -= OnPause;
        }

        private readonly IGameStateNavigator _gameStateNavigator;
        private readonly IBasicActionsProvider _actionsProvider;
        private readonly IPlayerActionsProvider _playerActionsProvider;
        private readonly IInGameMenuPresenter _inGameMenuPresenter;
        private readonly IGameConfig _gameConfig;
        private ILevelConfig? _levelConfig;
        private readonly ILevelItemsRegistry _levelItemsRegistry;

        private void OnLevelItemRegistered(int id, IHiddenObject hiddenObject)
        {
            hiddenObject.Clicked += OnHiddenObjectClicked;
        }
        private void OnLevelItemUnregistered(int id, IHiddenObject hiddenObject)
        {
            hiddenObject.Clicked -= OnHiddenObjectClicked;
        }
        private void OnHiddenObjectClicked(IHiddenObject hiddenObject)
        {
            var itemConfig = _levelConfig?.Items.FirstOrDefault(item => item.Id == hiddenObject.Config.Id);
            if (itemConfig != null)
            {
                Debug.Log($"{itemConfig.Name} clicked");
            }
        }
        private void OnPause()
        {
            _gameStateNavigator.PauseLevel();
        }
    }
}
