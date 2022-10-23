using Microsoft.Extensions.Options;

internal interface IPostService
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

    public PostService(IPostRepository repository, IPostClient client, IOptionsMonitor<Options> options)
    {
        _repository = repository;
        _client = client;
        _options = options.CurrentValue;
    }

    public async Task<IEnumerable<Post>> GetAll()
    {
        return _options.IsClient() ? await _client.GetAll() : _repository.GetAll();
    }

    public async Task<Post> GetById(Guid id)
    {
        return _options.IsClient() ? await _client.GetById(id) : _repository.GetById(id);
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
