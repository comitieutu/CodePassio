using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodePassio_Service.Interfaces;

namespace CodePassio_Admin.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<CodePassio_Core.Entities.Category> _categoryService;
        private readonly IMapper _mapper;

        public IndexModel(IRepository<CodePassio_Core.Entities.Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IList<CategoryViewModel> Category { get;set; }
        public class CategoryViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid? Parent { get; set; }
            public string ParentName { get; set; }
        }

        public void OnGet()
        {
            var categories = _categoryService.GetAll();
            Category = _mapper.Map<IList<CategoryViewModel>>(categories);
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
