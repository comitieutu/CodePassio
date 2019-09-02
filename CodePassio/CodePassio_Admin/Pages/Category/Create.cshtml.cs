using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodePassio_Core;
using CodePassio_Core.Entities;
using Microsoft.EntityFrameworkCore;
using CodePassio_Service.Interfaces;
using AutoMapper;
using CodePassio_Service.Services;

namespace CodePassio_Admin.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public CreateModel(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            var parent = _categoryService.Get(c => c.Parent == Guid.Empty).AsNoTracking().ToList();
            Categories = new List<SelectListItem>();
            parent.ForEach(c => Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name }));
            return Page();
        }

        [BindProperty]
        public CreateCategoryModel Category { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public class CreateCategoryModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid? Parent { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _categoryService.Create(_mapper.Map<CodePassio_Core.Entities.Category>(Category));

            return RedirectToPage("./Index");
        }
    }
}