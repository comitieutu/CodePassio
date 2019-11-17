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
using CodePassio_Service.Helpers;

namespace CodePassio_Admin.Pages.Post
{
    public class IndexModel : PageModel
    {
        private readonly PostService _postService;

        public IndexModel(PostService postService)
        {
            _postService = postService;
        }

        public PaginatedList<CodePassio_Core.Entities.Post> PublishedPost { get; set; }
        public PaginatedList<CodePassio_Core.Entities.Post> DraftPost { get; set; }
        public PaginatedList<CodePassio_Core.Entities.Post> DeletedPost { get; set; }

        public string TitleSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int PageSize { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int pageSize = 10)
        {
            PageSize = pageSize;
            CurrentSort = sortOrder;
            if (String.IsNullOrEmpty(sortOrder))
            {
                TitleSort = "title_desc";
            }
            else
            {
                TitleSort = sortOrder == "title_desc" ? "title_asc" : "title_desc";
            }
            
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            var publishedPost = _postService.GetAll().Where(p => p.Status == (byte)PostStatus.Publish).Include(p => p.Category).AsQueryable();
            var draftPost =  _postService.GetAll().Where(p => p.Status == (byte)PostStatus.Draft).Include(p => p.Category).AsQueryable();
            var deletedPost = _postService.GetAll().Where(p => p.Status == (byte)PostStatus.Trash).Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                publishedPost = publishedPost.Where(p => p.Title.Contains(searchString));
                draftPost = draftPost.Where(p => p.Title.Contains(searchString));
                deletedPost = deletedPost.Where(p => p.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    publishedPost = publishedPost.OrderByDescending(p => p.Title);
                    draftPost = draftPost.OrderByDescending(p => p.Title);
                    deletedPost = deletedPost.OrderByDescending(p => p.Title);
                    break;
                case "title_asc":
                    publishedPost = publishedPost.OrderBy(p => p.Title);
                    draftPost = draftPost.OrderBy(p => p.Title);
                    deletedPost = deletedPost.OrderBy(p => p.Title);
                    break;
                default:
                    publishedPost = publishedPost.OrderByDescending(p => p.ModifiedDate);
                    draftPost = draftPost.OrderByDescending(p => p.ModifiedDate);
                    deletedPost = deletedPost.OrderByDescending(p => p.ModifiedDate);
                    break;
            }

            PublishedPost = await PaginatedList<CodePassio_Core.Entities.Post>.CreateAsync(publishedPost, pageIndex ?? 1, pageSize);
            DraftPost = await PaginatedList<CodePassio_Core.Entities.Post>.CreateAsync(draftPost, pageIndex ?? 1, pageSize);
            DeletedPost = await PaginatedList<CodePassio_Core.Entities.Post>.CreateAsync(deletedPost, pageIndex ?? 1, pageSize);
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
                post.Status = (byte)PostStatus.Trash;
                _postService.DeleteEntity(post);
            }

            return RedirectToPage("./Index");
        }
    }
}
