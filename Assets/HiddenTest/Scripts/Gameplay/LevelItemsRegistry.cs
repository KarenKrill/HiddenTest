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
        public event Action<int>? ItemUnregistered;
        public event Action? RepositoryCleared;

        public void Register(int id, IHiddenObject hiddenObject)
        {
            _items[id] = hiddenObject;
            ItemRegistered?.Invoke(id, hiddenObject);
        }
        public void Unregister(int id)
        {
            _items.Remove(id);
            ItemUnregistered?.Invoke(id);
        }
        public void Clear()
        {
            _items.Clear();
            RepositoryCleared?.Invoke();
        }

        private readonly Dictionary<int, IHiddenObject> _items = new();
    }
}
