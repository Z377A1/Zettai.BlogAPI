using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Core.Exceptions;

namespace BlogAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repo.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var category = await _repo.GetByIdAsync(dto.Id) ?? throw new NotFoundException(nameof(Category), dto.Id);
            _mapper.Map(dto, category);
            _repo.Update(category);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Category), id);
            _repo.Delete(category);
            await _repo.SaveChangesAsync();
        }
    }
}
