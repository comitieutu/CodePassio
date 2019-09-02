using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Services;
using AutoMapper;

namespace CodePassio_Admin.Pages.Post
{
    public class EditModel : PageModel
    {
        private readonly PostService _postService;
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public EditModel(PostService postService, CategoryService categoryService, IMapper mapper)
        {
            _postService = postService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [BindProperty]
        public EditPostModel Post { get; set; }
        public List<SelectListItem> CategoryList { get; set; }

        public class EditPostModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }

            public string Excerpt { get; set; }

            public byte Status { get; set; }

            public string Content { get; set; }

            public string Tag { get; set; }

            public Guid CategoryId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postService.Entities
                .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (Post == null)
            {
                return NotFound();
            }

            Post = _mapper.Map<EditPostModel>(Post);

            CategoryList = new List<SelectListItem>();
            var categoriesData = _categoryService.Get(c => c.Parent == Guid.Empty).AsNoTracking().ToList();
            categoriesData.ForEach(c => CategoryList.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name }));

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = _mapper.Map<CodePassio_Core.Entities.Post>(Post);

            try
            {
                _postService.Edit(post);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_postService.IsExist(Post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
