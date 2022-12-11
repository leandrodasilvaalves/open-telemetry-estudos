using Demo.SharedModel.Contracts.Events;

namespace Demo.SharedModel.Events
{
    public abstract class EventBase<T> : IEventBase<T> where T : class
    {
        public string Name { get; protected set; }
        public DateTime TimeStamp { get; }
        public T Data { get; }

        public EventBase(T data)
        {
            TimeStamp = DateTime.UtcNow;
            Data = data;
        }
    }
}