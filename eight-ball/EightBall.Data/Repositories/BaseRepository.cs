using EightBall.Data.Entities;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
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
        public Task<Guid> AddEntityAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveEntityAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateEntityAsync(TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}