using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Core.Exceptions;

namespace BlogAPI.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _repo;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            var tags = await _repo.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public async Task<TagDto?> GetByIdAsync(int id)
        {
            var tag = await _repo.GetByIdAsync(id);
            return tag == null ? null : _mapper.Map<TagDto>(tag);
        }

        public async Task<TagDto> CreateAsync(CreateTagDto dto)
        {
            var tag = _mapper.Map<Tag>(dto);
            await _repo.AddAsync(tag);
            await _repo.SaveChangesAsync();
            return _mapper.Map<TagDto>(tag);
        }

        public async Task UpdateAsync(UpdateTagDto dto)
        {
            var tag = await _repo.GetByIdAsync(dto.Id) ?? throw new NotFoundException(nameof(Tag), dto.Id);
            _mapper.Map(dto, tag);
            _repo.Update(tag);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _repo.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Tag), id);
            _repo.Delete(tag);
            await _repo.SaveChangesAsync();
        }
    }
}
