using System.Text.Json;
namespace AspNet.Domain.Events;
public class PostWasConsulted
{
    protected PostWasConsulted() { }
    public PostWasConsulted(Post post)
    {
        Post = post;
        TimeStamp = DateTime.Now;
        EventId = Guid.NewGuid();
    }

    public Guid EventId { get; set; }
    public Post Post { get; set; }
    public DateTime TimeStamp { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}