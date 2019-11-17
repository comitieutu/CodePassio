using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodePassio_Service.Interfaces;
using CodePassio_Service.Services;
using CodePassio_Service.Helpers;
using AutoMapper.QueryableExtensions;

namespace CodePassio_Admin.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public IndexModel(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int PageSize { get; set; }
        public PaginatedList<CategoryViewModel> Category { get;set; }
        public class CategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid? Parent { get; set; }
            public string ParentName { get; set; }
        }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int pageSize = 10)
        {
            PageSize = pageSize;
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            var categories = _categoryService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(c => c.Name);
                    break;
                default:
                    categories = categories.OrderBy(c => c.Name);
                    break;
            }
            var categoryViewModels = _mapper.ProjectTo<CategoryViewModel>(categories);
            Category = await PaginatedList<CategoryViewModel>.CreateAsync(categoryViewModels, pageIndex ?? 1, pageSize);
            Category.ToList().ForEach(c =>
            {
                c.ParentName = c.Parent != Guid.Empty ? _categoryService.Get(c.Id).Name : c.ParentName;
            });
        }

        public IActionResult OnGetDelete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.Get(id);

            if (category != null && category.Parent != Guid.Empty)
            {
                _categoryService.Delete(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
