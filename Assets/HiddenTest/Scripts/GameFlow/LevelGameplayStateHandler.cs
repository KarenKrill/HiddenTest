#nullable enable
using System;
using System.Linq;
using UnityEngine;
using KarenKrill.UniCore.StateSystem.Abstractions;
using HiddenTest.Gameplay.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class LevelGameplayStateHandler : LevelGameplayStateHandlerBase, IStateHandler<GameState>
    {
        public LevelGameplayStateHandler(IGameStateNavigator gameStateNavigator,
            IInGameMenuPresenter inGameMenuPresenter,
            IPauseMenuPresenter pauseMenuPresenter,
            IGameConfig gameConfig,
            ILevelItemsRegistry levelItemsRegistry) :
            base(gameStateNavigator, inGameMenuPresenter, pauseMenuPresenter)
        {
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
                _levelConfig = _gameConfig.LevelsConfig[levelLoadContext.LevelIndex];
            }
            else
            {
                throw new ArgumentException($"Can't start {nameof(GameState.LevelGameplay)} without {nameof(LevelLoadContext)} context", nameof(context));
            }
        }

        public override void Exit(GameState nextState)
        {
            base.Exit(nextState);
        }

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
    }
}
