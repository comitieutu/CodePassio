using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Core.Entities
{
    public class PostTag : Entity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
