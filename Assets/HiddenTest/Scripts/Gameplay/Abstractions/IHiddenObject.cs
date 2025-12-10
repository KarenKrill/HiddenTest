#nullable enable
using System;
using UnityEngine;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IHiddenObject : IClickHandler
    {
        public GameObject ParentGameObject { get; }

        public event Action? Clicked;
    }
}
