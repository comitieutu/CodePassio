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
    public class ListModel : PageModel
    {
        private readonly PostService _postService;
        public ListModel(PostService postService)
        {
            _postService = postService;
        }
        public List<CodePassio_Core.Entities.Post> Posts { get; set; }
        public IActionResult OnGet(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Posts = _postService.GetAll().Include(p => p.Category).Where(p => p.CategoryId == id).AsNoTracking().OrderByDescending(p => p.ModifiedDate).ToList();

            return Page();
        }
    }
}