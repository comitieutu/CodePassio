using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;

namespace CodePassio_Service.Services
{
    public class CommentService : Repository<Comment>
    {
        public CommentService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
