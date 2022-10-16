namespace Exemplo5_Aspnet_ELK
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAll();
        Post GetById(Guid id);
        void Include(Post post);
    }

    public class PostRepository : IPostRepository
    {
        private readonly IList<Post> _database;
        public PostRepository() => _database = new List<Post>();

        public Post GetById(Guid id) => _database.FirstOrDefault(p => p.Id.Equals(id));

        public IEnumerable<Post> GetAll() => _database;

        public void Include(Post post) => _database.Add(post);
    }
}