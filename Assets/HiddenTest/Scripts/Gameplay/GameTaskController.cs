#nullable enable
using System;
using System.Collections.Generic;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    public class GameTaskController<ContextType> : IGameTaskController<ContextType> where ContextType : class
    {
        public IReadOnlyList<IGameTask<ContextType>> TaskList => _taskList;

        public event Action<IGameTask<ContextType>>? TaskAdded;
        public event Action<IGameTask<ContextType>>? TaskRemoved;
        public event Action? TaskCleared;

        public void AddTask(IGameTask<ContextType> task)
        {
            _taskList.Add(task);
            TaskAdded?.Invoke(task);
        }
        
        public void AddTask(string name, ContextType context) => AddTask(new GameTask<ContextType>(name, context));

        public void RemoveTask(IGameTask<ContextType> task)
        {
            if (_taskList.Remove(task))
            {
                TaskRemoved?.Invoke(task);
            }
        }

        public void ClearTasks()
        {
            _taskList.Clear();
            TaskCleared?.Invoke();
        }

        private readonly List<IGameTask<ContextType>> _taskList = new();
    }
}
