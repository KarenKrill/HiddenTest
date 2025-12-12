namespace HiddenTest.Gameplay
{
    using Abstractions;

    public class GameTask<ContextType> : IGameTask<ContextType> where ContextType : class
    {
        public string Name { get; }
        public ContextType Context { get; }

        public GameTask(string name, ContextType context)
        {
            Name = name;
            Context = context;
        }
    }
}
