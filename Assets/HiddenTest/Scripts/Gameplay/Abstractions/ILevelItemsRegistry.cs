#nullable enable
using System;
using System.Collections.Generic;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface ILevelItemsRegistry
    {
        public IReadOnlyDictionary<int, IHiddenObject> Items { get; }

        public event Action<int, IHiddenObject>? ItemRegistered;
        public event Action<int>? ItemUnregistered;
        public event Action? RepositoryCleared;

        public void Register(int id, IHiddenObject hiddenObject);
        public void Unregister(int id);
        public void Clear();
    }
}
