using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Core.Entities.DTOs.Identity;

namespace BlogAPI.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<ApplicationUser, ApplicationUserDto>();

            // BlogPost
            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BlogPostCategories.Select(bc => bc.Category)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.BlogPostTags.Select(bt => bt.Tag)))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
            CreateMap<CreateBlogPostDto, BlogPost>();
            CreateMap<UpdateBlogPostDto, BlogPost>();

            // Category & Tag
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore());
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<Tag, TagDto>()
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore());
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();

            // Comment
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
