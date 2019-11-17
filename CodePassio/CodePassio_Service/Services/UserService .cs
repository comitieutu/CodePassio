using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodePassio_Service.Services
{
    public class UserService : Repository<ApplicationUser>
    {
        public UserService(ApplicationDbContext context) : base(context)
        {
        }

        public void Delete(Guid id)
        {
            var user = Get(id);
            user.Deleted = true;
            Edit(user);
        }

        public new void Edit(ApplicationUser user)
        {
            user.ModifiedDate = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeleteEntity(ApplicationUser user)
        {
            user.Deleted = true;
            Edit(user);
        }
    }
}
