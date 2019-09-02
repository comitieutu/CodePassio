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

        public async void CreateDraftAsync(CodePassio_Core.Entities.Post post)
        {            
            post.Status = (byte)PostStatus.Draft;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
