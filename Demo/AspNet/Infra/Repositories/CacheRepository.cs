using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Distributed;

internal class CacheRepository : IPostRepository
{
    private readonly IDistributedCache _database;
    private readonly IPostRepository _nextRepository;
    private readonly RedisOptions _options;

    public CacheRepository(IOptionsMonitor<Options> options,
                           IDistributedCache database = null,
                           IPostRepository nextRepository = null)
    {
        _options = options.CurrentValue?.Redis;
        _database = database;
        _nextRepository = nextRepository;
    }

    private async Task SaveAsync(Post post)
    {
        var cacheOptions = new DistributedCacheEntryOptions();
        cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(_options.TTL));
        var json = JsonSerializer.Serialize(post);
        await _database.SetStringAsync(post.Id.ToString(), json, cacheOptions);
    }

    private async Task<Post> GetAsync(Guid postId)
    {
        var post = await _database.GetStringAsync(postId.ToString());
        return JsonSerializer.Deserialize<Post>(post);
    }

    public IEnumerable<Post> GetAll() => _nextRepository.GetAll();

    public Post GetById(Guid id)
    {
        var post = GetAsync(id).Result;
        if (post == null)
        {
            post = _nextRepository.GetById(id);
            SaveAsync(post).Wait();
        }
        return post;
    }

    public void Include(Post post) => _nextRepository.Include(post);
}
