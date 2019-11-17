using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace CodePassio_Service.Services
{
    public class CategoryService : Repository<Category>
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
        public void Delete(Guid id)
        {
            var category = Get(id);
            category.Deleted = true;
            Edit(category);
        }

        public new void Edit(Category category)
        {
            category.ModifiedDate = DateTime.Now;
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeleteEntity(Category category)
        {
            category.Deleted = true;
            Edit(category);
        }
    }
}
