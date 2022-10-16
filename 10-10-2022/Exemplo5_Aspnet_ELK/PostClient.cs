using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Exemplo5_Aspnet_ELK
{
    internal interface IPostClient
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(Guid id);
        Task Post(Post post);
    }


    internal class PostClient : IPostClient
    {
        private HttpClient _client;
        private Options _options;

        public PostClient(IHttpClientFactory factory, IOptionsMonitor<Options> options)
        {
            _client = factory.CreateClient();
            _options = options.CurrentValue;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<Post>>($"{_options.UrlClient}/posts");
            await Task.Delay(new Random().Next(100, _options.MaxDelayMileseconds));
            return response;
        }

        public async Task<Post> GetById(Guid id)
        {
            var response = await _client.GetFromJsonAsync<Post>($"{_options.UrlClient}/posts/{id}");
            await Task.Delay(new Random().Next(100, _options.MaxDelayMileseconds));
            return response;
        }

        public async Task Post(Post post)
        {
            var json = JsonSerializer.Serialize(post);
            var response = await _client.PostAsync($"{_options.UrlClient}/posts", new StringContent(json, Encoding.UTF8, "application/json"));
            await Task.Delay(new Random().Next(100, _options.MaxDelayMileseconds));
        }
    }
}