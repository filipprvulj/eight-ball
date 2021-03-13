using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.RepositoryInterfaces
{
    public interface IBaseRepository<TDto> where TDto : BaseDto
    {
        public Task<TDto> GetByIdAsync(Guid id);

        public Task<List<TDto>> GetEntitiesAsync();

        public Task<int> RemoveEntityAsync(Guid id);

        public Task<Guid> AddEntityAsync(TDto dto);

        public Task<int> UpdateEntityAsync(TDto dto);
    }
}