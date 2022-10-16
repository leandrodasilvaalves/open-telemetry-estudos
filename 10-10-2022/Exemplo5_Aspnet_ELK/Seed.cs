using Bogus;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Options;

namespace Exemplo5_Aspnet_ELK
{
    internal class Seed : IAsyncInitializer
    {
        private IPostRepository _repository;
        private readonly Options _options;
        private readonly Faker<Post> _faker;

        public Seed(IPostRepository repository, IOptionsMonitor<Options> options)
        {
            _repository = repository;
            _options = options.CurrentValue;

            _faker = new Faker<Post>()
                .RuleFor(p => p.Id, Guid.NewGuid())
                .RuleFor(p => p.UserId, Guid.NewGuid())
                .RuleFor(p => p.Title, f => f.Random.Words())
                .RuleFor(p => p.Body, f => f.Random.Words(_options.Seed?.MaxBodyWords));
        }

        public async Task InitializeAsync()
        {
            if (_options.IsServer())
            {
                var posts = _faker.Generate(_options.Seed.Records);
                foreach (var post in posts)
                {
                    _repository.Include(post);
                }
            }
            await Task.CompletedTask;
        }
    }
}