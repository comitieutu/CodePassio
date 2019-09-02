using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Services;

namespace CodePassio_Admin.Pages.Post
{
    public class IndexModel : PageModel
    {
        private readonly PostService _postService;

        public IndexModel(PostService postService)
        {
            _postService = postService;
        }

        public IList<CodePassio_Core.Entities.Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _postService.GetAll()
                .Include(p => p.Category).ToListAsync();
        }
    }
}
