#nullable enable
using System;
using UnityEngine;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IHiddenObject : IClickHandler
    {
        public IItemConfig Config { get; }
        public GameObject ParentGameObject { get; }

        public event Action<IHiddenObject>? Clicked;
    }
}
