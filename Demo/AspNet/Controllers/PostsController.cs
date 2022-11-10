using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNet.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly ActivitySource _source;
        private readonly ILogger<PostsController> _logger;    
        private Options _options;

        public PostsController(IPostService postService, IOptionsMonitor<Options> options, ILogger<PostsController> logger)
        {
            _service = postService;
            _options = options.CurrentValue;
            _source = new ActivitySource(_options.ServiceName);
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var activity = _source.StartActivity("GET /posts");
            activity?.SetTag("before_request", "get all posts");

            _logger.LogInformation("Starting process");
            var response = await _service.GetAll();
            await Task.Delay(new Random().Next(100, _options.MaxDelayMileseconds));

            activity?.SetTag("after_request", "get all posts");
            _logger.LogInformation("Ending process");
            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = _source.StartActivity($"GET /posts/{id}");
            activity?.SetTag("before_request", $"post_id:{id}");

            var response = await _service.GetById(id);
            await Task.Delay(new Random().Next(100, _options.MaxDelayMileseconds));

            activity?.SetTag("after_request", $"post_id:{id}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            var activity = _source.StartActivity($"POST /posts");
            activity?.SetTag("before_request", $"post_id:{post.Id}");

            await _service.Include(post);
            await Task.Delay(new Random().Next(100, _options.MaxDelayMileseconds));

            activity?.SetTag("after_request", $"post_id:{post.Id}");
            return Ok();
        }
    }
}