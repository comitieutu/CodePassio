using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodePassio_Core;
using CodePassio_Core.Entities;

namespace CodePassio_Admin.Pages.Tag
{
    public class IndexModel : PageModel
    {
        private readonly CodePassio_Core.ApplicationDbContext _context;

        public IndexModel(CodePassio_Core.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CodePassio_Core.Entities.Tag> Tag { get;set; }

        public async Task OnGetAsync()
        {
            Tag = await _context.Tags.ToListAsync();
        }
    }
}
