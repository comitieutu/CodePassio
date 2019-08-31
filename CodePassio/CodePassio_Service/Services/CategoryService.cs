using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;

namespace CodePassio_Service.Services
{
    public class CategoryService : Repository<Category>
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
