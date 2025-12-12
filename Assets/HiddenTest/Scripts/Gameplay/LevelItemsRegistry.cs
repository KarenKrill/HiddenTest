#nullable enable
using System;
using System.Collections.Generic;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    public class LevelItemsRegistry : ILevelItemsRegistry
    {
        public IReadOnlyDictionary<int, IHiddenObject> Items => _items;

        public event Action<int, IHiddenObject>? ItemRegistered;
        public event Action<int, IHiddenObject>? ItemUnregistered;

        public LevelItemsRegistry(IGameConfig gameConfig, ILevelSession levelSession)
        {
            _gameConfig = gameConfig;
            levelSession.ActiveLevelChanged += OnActiveLevelChanged;
        }

        public void Register(int id, IHiddenObject hiddenObject)
        {
            if (_currentLevelConfig?.EnabledItems.ContainsKey(id) ?? false)
            {
                _items[id] = hiddenObject;
                ItemRegistered?.Invoke(id, hiddenObject);
            }
        }
        public void Unregister(int id)
        {
            if (_items.Remove(id, out var hiddenObject))
            {
                ItemUnregistered?.Invoke(id, hiddenObject);
            }
        }

        private readonly IGameConfig _gameConfig;
        private readonly Dictionary<int, IHiddenObject> _items = new();
        private ILevelConfig? _currentLevelConfig;

        private void OnActiveLevelChanged(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < _gameConfig.LevelsConfig.Count)
            {
                _currentLevelConfig = _gameConfig.LevelsConfig[levelIndex];
            }
            else
            {
                _currentLevelConfig = null;
            }
        }
    }
}
