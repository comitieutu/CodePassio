using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? Parent { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
