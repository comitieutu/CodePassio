using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePassio_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodePassio_Web.Pages.Post
{
    public class DetailModel : PageModel
    {
        private readonly PostService _postService;
        public DetailModel(PostService postService)
        {
            _postService = postService;
        }
        public CodePassio_Core.Entities.Post Post { get; set; }
        public IActionResult OnGet(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = _postService.Get(p => p.Id == id).Include(p => p.Category).FirstOrDefault();

            return Page();
        }
    }
}