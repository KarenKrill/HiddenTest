#nullable enable

using System;

using UnityEngine;

using KarenKrill.UniCore.Input.Abstractions;

namespace HiddenTest.Input.Abstractions
{
    public interface IPlayerActionsProvider
    {
        public Vector2 LastMoveDelta { get; }
        public bool IsAttackActive { get; }

        public event Action? MoveStarted;
        public event MoveDelegate? Move;
        public event Action? MoveCancel;
        public event Action? Attack;
        public event Action? AttackCancel;
        public event Action? Pause;
        public event Action? Previous;
        public event Action? Next;
    }
}
