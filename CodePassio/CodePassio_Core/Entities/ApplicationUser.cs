using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CodePassio_Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
