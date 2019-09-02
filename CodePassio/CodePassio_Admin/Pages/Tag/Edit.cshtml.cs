using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodePassio_Core;
using CodePassio_Core.Entities;
using AutoMapper;
using CodePassio_Service.Interfaces;
using CodePassio_Service.Services;

namespace CodePassio_Admin.Pages.Tag
{
    public class EditModel : PageModel
    {
        private readonly TagService _tagService;
        private readonly IMapper _mapper;

        public EditModel(TagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [BindProperty]
        public EditTagModel Tag { get; set; }

        public class EditTagModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _tagService.Get(id);
            Tag = _mapper.Map<EditTagModel>(tag);

            if (Tag == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var tag = _mapper.Map<CodePassio_Core.Entities.Tag>(Tag);

            try
            {
                _tagService.Edit(tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_tagService.IsExist(Tag.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
