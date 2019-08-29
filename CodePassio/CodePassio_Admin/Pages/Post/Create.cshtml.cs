using System;
using System.Collections.Generic;
using System.Linq;
using CodePassio_Service.Services.Post;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodePassio_Core;
using CodePassio_Core.Entities;

namespace CodePassio_Admin.Pages.Post
{
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly CodePassio_Core.ApplicationDbContext _context;

        // Pass to view properties
        public List<SelectListItem> CategoryList { get; set; }

        public CreateModel(CodePassio_Core.ApplicationDbContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }

        public IActionResult OnGet()
        {
            CategoryList = new List<SelectListItem>();
            var CategoriesData = _context.Categories.ToList();
            CategoriesData.ForEach(c => CategoryList.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name }));

            return Page();
        }

        [BindProperty]
        public CodePassio_Core.Entities.Post Post { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDraftAsync()
        {
            _postService.CreateDraftAsync(Post);
            return RedirectToPage("./Post");
        }
    }
}