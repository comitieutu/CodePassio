using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodePassio_Core;
using CodePassio_Core.Entities;
using AutoMapper;
using CodePassio_Service.Interfaces;

namespace CodePassio_Admin.Pages.Tag
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<CodePassio_Core.Entities.Tag> _tagService;
        private readonly IMapper _mapper;

        public CreateModel(IRepository<CodePassio_Core.Entities.Tag> tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateTagModel Tag { get; set; }

        public class CreateTagModel
        {
            public string Name { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _tagService.Create(_mapper.Map<CodePassio_Core.Entities.Tag>(Tag));

            return RedirectToPage("./Index");
        }
    }
}