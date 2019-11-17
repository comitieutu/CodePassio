using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePassio_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodePassio_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PostService _postService;        
        public IndexModel(PostService postService)
        {
            _postService = postService;
        }

        public IList<CodePassio_Core.Entities.Post> Posts { get; set; }

        public IActionResult OnGet()
        {
            Posts = _postService.GetAll().Include(p => p.Category).Include(p => p.Comments).ToList();
            return Page();
        }
    }
}
