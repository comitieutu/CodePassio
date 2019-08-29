using CodePassio_Core.Entities;
using CodePassio_Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodePassio_Service.Services.Post
{
    public class PostService : IPostService
    {
        private readonly CodePassio_Core.ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public PostService(CodePassio_Core.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async void CreateDraftAsync(CodePassio_Core.Entities.Post post)
        {            
            post.Status = (byte)PostStatus.Draft;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
