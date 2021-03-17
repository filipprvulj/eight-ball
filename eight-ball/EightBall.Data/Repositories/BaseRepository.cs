using AutoMapper;
using EightBall.Data.Entities;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Data.Repositories
{
    public class BaseRepository<TEntity, TDto> : IBaseRepository<TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        protected readonly EightBallDbContext _context;
        protected readonly IMapper _mapper;
        protected DbSet<TEntity> Entities { get; set; }

        public BaseRepository(EightBallDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Entities = _context.Set<TEntity>();
        }

        public async Task<Guid> AddEntityAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            Entities.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public Task<List<TDto>> GetEntitiesAsync()
        {
            return _mapper.ProjectTo<TDto>(Entities).ToListAsync();
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id)
        {
            Task<TEntity> entity = Entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<TDto>(await entity);
        }

        public async Task<int> RemoveEntityAsync(Guid id)
        {
            TEntity entity = await Entities.FindAsync(id);
            Entities.Remove(entity);

            return await _context.SaveChangesAsync();
        }

        public Task<int> UpdateEntityAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            Entities.Update(entity);

            return _context.SaveChangesAsync();
        }

        public Task<bool> EntityExists(Guid id)
        {
            return Entities.AnyAsync(e => e.Id == id);
        }
    }
}