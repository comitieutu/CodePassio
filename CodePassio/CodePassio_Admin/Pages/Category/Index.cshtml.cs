using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CodePassio_Admin.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly CodePassio_Core.ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IndexModel(CodePassio_Core.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
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

        public async Task OnGetAsync()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            Category = _mapper.Map<IList<CategoryViewModel>>(categories);
            Category.ToList().ForEach(c =>
            {
                c.ParentName = c.Parent != Guid.Empty ? _context.Categories.Find(c.Parent).Name : c.ParentName;
            });
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category != null && category.Parent != Guid.Empty)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
