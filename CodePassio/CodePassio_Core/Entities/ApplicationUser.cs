using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CodePassio_Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}
