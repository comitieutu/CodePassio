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
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
