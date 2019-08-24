using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CodePassio_Admin.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly CodePassio_Core.ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EditModel(CodePassio_Core.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
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

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            InputModel = _mapper.Map<EditCategory>(category);

            if (category == null)
            {
                return NotFound();
            }

            var cates = _context.Categories.AsNoTracking().Where(c => c.Parent == Guid.Empty).ToList();
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = _mapper.Map<CodePassio_Core.Entities.Category>(InputModel);
            category.ModifiedDate = DateTime.Now;
            _context.Attach(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(InputModel.Id))
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

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
