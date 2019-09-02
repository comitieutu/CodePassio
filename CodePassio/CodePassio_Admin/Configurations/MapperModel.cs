using AutoMapper;
using CodePassio_Core.Entities;
using static CodePassio_Admin.Pages.Category.CreateModel;
using static CodePassio_Admin.Pages.Category.EditModel;
using static CodePassio_Admin.Pages.Category.IndexModel;
using static CodePassio_Admin.Pages.Post.CreateModel;
using static CodePassio_Admin.Pages.Post.EditModel;
using static CodePassio_Admin.Pages.Tag.CreateModel;
using static CodePassio_Admin.Pages.Tag.EditModel;

namespace CodePassio_Admin.Configurations
{
    public class MapperModel : Profile
    {
        public MapperModel()
        {
            //category
            CreateMap<Category, CreateCategoryModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, EditCategory>().ReverseMap();

            //Tag
            CreateMap<CreateTagModel, Tag>();
            CreateMap<EditTagModel, Tag>().ReverseMap();

            //Post
            CreateMap<Post, CreatePostModel>();
            CreateMap<EditPostModel, Post>().ReverseMap();
        }
    }
}
