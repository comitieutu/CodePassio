using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CodePassio_Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Comment Comment { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }
    }
}
