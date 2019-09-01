using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Services;
using CodePassio_Service.Interfaces;

namespace CodePassio_Admin.Pages.Tag
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<CodePassio_Core.Entities.Tag> _tagService;

        public IndexModel(IRepository<CodePassio_Core.Entities.Tag> tagService)
        {
            _tagService = tagService;
        }

        public IList<CodePassio_Core.Entities.Tag> Tag { get;set; }

        public async Task OnGetAsync()
        {
            Tag = await _tagService.GetAll().ToListAsync();
        }

        public IActionResult OnGetDelete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagService.Get(id);

            if (tag != null)
            {
                _tagService.Delete(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
