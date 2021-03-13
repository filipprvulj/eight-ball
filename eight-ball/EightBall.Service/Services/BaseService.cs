using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Service.Services
{
    public class BaseService<TDto, TRepository> : IBaseService<TDto>
        where TDto : BaseDto
        where TRepository : IBaseRepository<TDto>
    {
        protected readonly TRepository _repository;

        public BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> EntityExists(Guid id)
        {
            return _repository.EntityExists(id);
        }

        public Task<TDto> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<List<TDto>> GetEntitiesAsync()
        {
            return _repository.GetEntitiesAsync();
        }

        public Task<Guid> InsertAsync(TDto dto)
        {
            return _repository.AddEntityAsync(dto);
        }

        public Task<int> RemoveAsync(Guid id)
        {
            return _repository.RemoveEntityAsync(id);
        }

        public Task<int> UpdateAsync(TDto dto)
        {
            return _repository.UpdateEntityAsync(dto);
        }
    }
}