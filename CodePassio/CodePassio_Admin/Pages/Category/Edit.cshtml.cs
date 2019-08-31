using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodePassio_Service.Interfaces;

namespace CodePassio_Admin.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly IRepository<CodePassio_Core.Entities.Category> _categoryService;
        private readonly IMapper _mapper;

        public EditModel(IRepository<CodePassio_Core.Entities.Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [BindProperty]
        public EditCategory InputModel { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public class EditCategory
        {
            public Guid Id { get; set; }
            public string CategoryName { get; set; }
            public string CategoryDes { get; set; }
            public Guid? Parent { get; set; }
        }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.Get(id);
            InputModel = _mapper.Map<EditCategory>(category);

            if (category == null)
            {
                return NotFound();
            }

            var cates = _categoryService.Get(c => c.Parent == Guid.Empty).AsNoTracking().ToList();
            Categories = new List<SelectListItem>();
            cates.ForEach(c =>
            {
                if (c.Id == id)
                {
                    Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = true });
                }
                else
                {
                    Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                }
            });

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = _mapper.Map<CodePassio_Core.Entities.Category>(InputModel);

            try
            {
                _categoryService.Edit(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_categoryService.IsExist(InputModel.Id))
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
