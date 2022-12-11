namespace Demo.SharedModel.Events
{
    public abstract class EventBase
    {
        public string Name { get; protected set; }
        public DateTime TimeStamp { get; }
        public Object Data { get; }

        public EventBase(object data)
        {
            TimeStamp = DateTime.UtcNow;
            Data = data;
        }
    }
}