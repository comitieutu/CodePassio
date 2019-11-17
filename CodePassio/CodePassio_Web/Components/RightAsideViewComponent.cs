using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePassio_Web.Components
{
    public class RightAsideViewComponent : ViewComponent
    {
        private readonly TagService _tagService;

        public RightAsideViewComponent(TagService tagService)
        {
            _tagService = tagService;
        }

        public class RightAsideView
        {
            public List<string> TagName { get; set; }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(GetItems());
        }
        private RightAsideView GetItems()
        {
            var tagName = _tagService.GetAll().Take(10).Select(t => t.Name).ToList();
            var rightAsideView = new RightAsideView
            {
                TagName = tagName
            };
            return rightAsideView;
        }
    }
}
