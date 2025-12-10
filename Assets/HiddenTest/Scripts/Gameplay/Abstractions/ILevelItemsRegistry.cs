#nullable enable
using System;
using System.Collections.Generic;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface ILevelItemsRegistry
    {
        public IReadOnlyDictionary<int, IHiddenObject> Items { get; }

        public event Action<int, IHiddenObject>? ItemRegistered;
        public event Action<int, IHiddenObject>? ItemUnregistered;

        public void Register(int id, IHiddenObject hiddenObject);
        public void Unregister(int id);
    }
}
