using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodePassio_Service.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CodePassio_Service.Implementations;

namespace CodePassio_Admin.Pages.Post
{
    public class CreateModel : PageModel
    {
        private readonly PostService _postService;
        private readonly IMapper _mapper;
        private readonly CategoryService _categoryService;
        private readonly TagService _tagService;

        public CreateModel(PostService postService, IMapper mapper, CategoryService categoryService, TagService tagService)
        {
            _postService = postService;
            _mapper = mapper;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public IActionResult OnGet()
        {
            CategoryList = new List<SelectListItem>();
            var categoriesData = _categoryService.Get(c => c.Parent != Guid.Empty).AsNoTracking().ToList();
            categoriesData.ForEach(c => CategoryList.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name }));

            return Page();
        }

        [BindProperty]
        public CreatePostModel Post { get; set; }
        public List<SelectListItem> CategoryList { get; set; }

        public class CreatePostModel
        {
            public string Title { get; set; }

            public string Excerpt { get; set; }

            public byte Status { get; set; }

            public string Content { get; set; }

            public string Tag { get; set; }

            public Guid CategoryId { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = _mapper.Map<CodePassio_Core.Entities.Post>(Post);
            _postService.Create(post);

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostDraft()
        {
            var post = _mapper.Map<CodePassio_Core.Entities.Post>(Post);
            _postService.CreateDraftAsync(post);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetTag(string term)
        {
            List<string> tag = _tagService.GetAll().AsNoTracking().Select(t => t.Name).ToList();
            return new JsonResult(tag);
        }
    }
}