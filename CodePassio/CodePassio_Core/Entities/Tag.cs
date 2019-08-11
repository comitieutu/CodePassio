using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
