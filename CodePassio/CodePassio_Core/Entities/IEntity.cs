using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Core.Entities
{
    public interface IEntity
    {       
        bool Deleted { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }

    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }
    }
    public abstract class Entity : IEntity
    {
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }
    }
}
