using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Core.Entities.DTOs.Identity;

namespace BlogAPI.Services.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(d => d.Categories, o => o.MapFrom(s => s.BlogPostCategories.Select(bc => bc.Category)))
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.BlogPostTags.Select(bt => bt.Tag)))
                .ForMember(d => d.Comments, o => o.MapFrom(s => s.Comments));
            CreateMap<CreateBlogPostDto, BlogPost>();
            CreateMap<UpdateBlogPostDto, BlogPost>();
            CreateMap<Category, CategoryDto>().ForMember(d => d.BlogPosts, o => o.Ignore());
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Tag, TagDto>().ForMember(d => d.BlogPosts, o => o.Ignore());
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
