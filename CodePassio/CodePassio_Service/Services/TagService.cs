using CodePassio_Core;
using CodePassio_Core.Entities;
using CodePassio_Service.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Service.Services
{
    public class TagService : Repository<Tag>
    {
        public TagService(ApplicationDbContext context) : base(context)
        {
        }

    }
}
