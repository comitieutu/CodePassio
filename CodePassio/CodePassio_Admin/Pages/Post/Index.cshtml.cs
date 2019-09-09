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
using CodePassio_Core.Enums;

namespace CodePassio_Admin.Pages.Post
{
    public class IndexModel : PageModel
    {
        private readonly PostService _postService;

        public IndexModel(PostService postService)
        {
            _postService = postService;
        }

        public IList<CodePassio_Core.Entities.Post> PublishedPost { get; set; }
        public IList<CodePassio_Core.Entities.Post> DraftPost { get; set; }
        public IList<CodePassio_Core.Entities.Post> DeletedPost { get; set; }

        public async Task OnGetAsync()
        {
            PublishedPost = await _postService.GetAll().Where(p => p.Status == (byte)PostStatus.Publish).Include(p => p.Category).ToListAsync();
            DraftPost = await _postService.GetAll().Where(p => p.Status == (byte)PostStatus.Draft).Include(p => p.Category).ToListAsync();
            DeletedPost = await _postService.GetAll().Where(p => p.Status == (byte)PostStatus.Trash).Include(p => p.Category).ToListAsync();
        }

        public IActionResult OnGetDelete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postService.Get(id);

            if (post != null)
            {
                _postService.Delete(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
