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

namespace CodePassio_Admin.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly CodePassio_Core.ApplicationDbContext _context;

        public CreateModel(CodePassio_Core.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var parent = _context.Categories.AsNoTracking().Where(c => c.Parent == Guid.Empty).ToList();
            Categories = new List<SelectListItem>();
            parent.ForEach(c => Categories.Add(new SelectListItem { Value = c.Id.ToString(), Text = c.Name }));
            return Page();
        }

        [BindProperty]
        public CodePassio_Core.Entities.Category Category { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}