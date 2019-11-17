using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodePassio_Service.Services
{
    public class CommentService : Repository<Comment>
    {
        public CommentService(ApplicationDbContext context) : base(context)
        {
        }
        public void Delete(Guid id)
        {
            var comment = Get(id);
            comment.Deleted = true;
            Edit(comment);
        }

        public new void Edit(Comment comment)
        {
            comment.ModifiedDate = DateTime.Now;
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeleteEntity(Comment comment)
        {
            comment.Deleted = true;
            Edit(comment);
        }
    }
}
