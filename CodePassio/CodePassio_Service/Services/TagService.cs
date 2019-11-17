using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Service.Services
{
    public class TagService : Repository<Tag>
    {
        public TagService(ApplicationDbContext context) : base(context)
        {
        }

        public void Delete(Guid id)
        {
            var tag = Get(id);
            tag.Deleted = true;
            Edit(tag);
        }

        public new void Edit(Tag tag)
        {
            tag.ModifiedDate = DateTime.Now;
            _context.Entry(tag).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeleteEntity(Tag tag)
        {
            tag.Deleted = true;
            Edit(tag);
        }

    }
}
