using AutoMapper;
using CodePassio_Core.Entities;
using static CodePassio_Admin.Pages.Category.EditModel;
using static CodePassio_Admin.Pages.Category.IndexModel;

namespace CodePassio_Admin.Configurations
{
    public class MapperModel : Profile
    {
        public MapperModel()
        {
            //category
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, EditCategory>().ReverseMap();
        }
    }
}
