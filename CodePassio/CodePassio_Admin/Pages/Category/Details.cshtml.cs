using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodePassio_Core;
using CodePassio_Core.Entities;
using static CodePassio_Admin.Pages.Category.IndexModel;
using AutoMapper;

namespace CodePassio_Admin.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly CodePassio_Core.ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetailsModel(CodePassio_Core.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CategoryViewModel Category { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cate = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (cate == null)
            {
                return NotFound();
            }

            Category = _mapper.Map<CategoryViewModel>(cate);
            Category.ParentName = cate.Parent != Guid.Empty ? _context.Categories.Find(cate.Id).Name : cate.Name;

            return Page();
        }
    }
}
