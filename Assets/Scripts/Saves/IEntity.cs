namespace Game.Saves
{
    public interface IEntity<T> : IPersistentData
    {
        void Update(T value);
    }

    public interface IPersistentData
    {
        long saveKey { get; }
        void BuildIndex();
    }
}
