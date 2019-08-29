using System;
using System.Collections.Generic;
using System.Text;

namespace CodePassio_Service.Services.Post
{
    public interface IPostService
    {
        void CreateDraftAsync(CodePassio_Core.Entities.Post post);
    }
}
