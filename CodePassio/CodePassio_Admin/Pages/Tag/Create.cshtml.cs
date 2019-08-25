using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodePassio_Core;
using CodePassio_Core.Entities;

namespace CodePassio_Admin.Pages.Tag
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
            return Page();
        }

        [BindProperty]
        public CodePassio_Core.Entities.Tag Tag { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tags.Add(Tag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}