using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Core.Enums;
using CodePassio_Service.Implementations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodePassio_Service.Services
{
    public class PostService : Repository<Post>
    {

        public PostService(ApplicationDbContext context) : base(context)
        {
        }

        public void CreateDraftAsync(CodePassio_Core.Entities.Post post)
        {            
            post.Status = (byte)PostStatus.Draft;
            post.ModifiedDate = DateTime.Now;
            this._context.Posts.Add(post);
            this._context.Entry(post).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            this._context.SaveChanges();
        }
    }
}
