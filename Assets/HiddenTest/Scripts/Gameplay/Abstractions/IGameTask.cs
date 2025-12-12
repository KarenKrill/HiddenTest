namespace HiddenTest.Gameplay.Abstractions
{
    public interface IGameTask<ContextType> where ContextType : class
    {
        public string Name { get; }
        public ContextType Context { get; }
    }
}
