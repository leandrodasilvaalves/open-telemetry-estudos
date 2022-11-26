using AspNet.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Options;

public interface IPostService
{
    Task<IEnumerable<Post>> GetAll();
    Task<Post> GetById(Guid id);
    Task Include(Post post);
}

internal class PostService : IPostService
{
    private readonly IPostRepository _repository;
    private readonly IPostClient _client;
    private readonly Options _options;
    private readonly IBus _bus;

    public PostService(IPostRepository repository,
                       IPostClient client,
                       IOptionsMonitor<Options> options,
                       IBus bus)
    {
        _repository = repository;
        _client = client;
        _options = options.CurrentValue;
        _bus = bus;
    }

    public async Task<IEnumerable<Post>> GetAll()
    {
        return _options.IsClient() ? await _client.GetAll() : _repository.GetAll();
    }

    public async Task<Post> GetById(Guid id)
    {
        var post = _options.IsClient() ? await _client.GetById(id) : _repository.GetById(id);
        await _bus.Publish(new PostWasConsulted(post));
        return post;
    }

    public async Task Include(Post post)
    {
        if (_options.IsClient())
        {
            await _client.Post(post);
        }
        else
        {
            _repository.Include(post);
        }
    }
}
