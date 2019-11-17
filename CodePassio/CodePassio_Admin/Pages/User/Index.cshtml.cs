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
using CodePassio_Core.Entities;

namespace CodePassio_Admin.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public IndexModel(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IList<UserListViewModel> Users { get;set; }
        public class UserListViewModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        public void OnGet()
        {
            var users = _userService.GetAll();
            Users = _mapper.Map<IList<UserListViewModel>>(users);
        }

        public IActionResult OnGetDelete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = _categoryService.Get(id);

            //if (category != null && category.Parent != Guid.Empty)
            //{
            //    _categoryService.Delete(id);
            //}

            return RedirectToPage("./Index");
        }
    }
}
