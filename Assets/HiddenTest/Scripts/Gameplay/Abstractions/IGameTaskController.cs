#nullable enable
using System;
using System.Collections.Generic;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IGameTaskController<ContextType> where ContextType : class
    {
        public IReadOnlyList<IGameTask<ContextType>> TaskList { get; }

        public event Action<IGameTask<ContextType>>? TaskAdded;
        public event Action<IGameTask<ContextType>>? TaskRemoved;
        public event Action? TaskCleared;

        public void AddTask(IGameTask<ContextType> task);
        public void AddTask(string name, ContextType context);
        public void RemoveTask(IGameTask<ContextType> task);
        public void ClearTasks();
    }
}
