using BlogAPI.Core.Entities.DTOs;

namespace BlogAPI.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<TagDto?> GetByIdAsync(int id);
        Task<TagDto> CreateAsync(CreateTagDto dto);
        Task UpdateAsync(UpdateTagDto dto);
        Task DeleteAsync(int id);
    }
}
