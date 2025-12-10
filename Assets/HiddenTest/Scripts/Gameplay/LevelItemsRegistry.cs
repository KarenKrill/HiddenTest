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

        public void Register(int id, IHiddenObject hiddenObject)
        {
            _items[id] = hiddenObject;
            ItemRegistered?.Invoke(id, hiddenObject);
        }
        public void Unregister(int id)
        {
            if (_items.Remove(id, out var hiddenObject))
            {
                ItemUnregistered?.Invoke(id, hiddenObject);
            }
        }

        private readonly Dictionary<int, IHiddenObject> _items = new();
    }
}
