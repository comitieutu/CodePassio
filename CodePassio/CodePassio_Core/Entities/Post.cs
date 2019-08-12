using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Core.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Excerpt { get; set; }

        public byte Status { get; set; }

        public string Content { get; set; }

        public string Tag { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
