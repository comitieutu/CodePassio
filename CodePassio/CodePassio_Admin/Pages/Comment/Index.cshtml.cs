using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CodePassio_Service.Interfaces;
using CodePassio_Service.Services;

namespace CodePassio_Admin.Pages.Comment
{
    public class IndexModel : PageModel
    {
        private readonly CommentService _commentService;

        public IndexModel(CommentService commentService)
        {
            _commentService = commentService;
        }

        public IList<CodePassio_Core.Entities.Comment> Comments { get;set; }

        public async Task OnGetAsync()
        {
            Comments = await _commentService.GetAll().ToListAsync();
        }
    }
}
