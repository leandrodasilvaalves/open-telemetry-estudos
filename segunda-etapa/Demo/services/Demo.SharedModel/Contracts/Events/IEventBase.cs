namespace Demo.SharedModel.Contracts.Events
{
    public interface IEventBase<T> where T : class
    {
        public string Name { get; }
        public DateTime TimeStamp { get; }
        public T Data { get; }
    }
}