using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
