using EightBall.Shared;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
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

        public async Task<Result<TDto>> GetByIdAsync(Guid id)
        {
            Result<TDto> result = new Result<TDto>();

            var entityExists = await EntityExists(id);
            if (!entityExists)
            {
                result.Errors.Add(Errors.NotFound, $"{typeof(TDto).Name} not found");

                return result;
            }

            result.Succeeded = true;
            result.Value = await _repository.GetByIdAsync(id);
            return result;
        }

        public async Task<Result<List<TDto>>> GetEntitiesAsync()
        {
            Result<List<TDto>> result = new Result<List<TDto>>();
            var tables = _repository.GetEntitiesAsync();

            result.Succeeded = true;
            result.Value = await tables;
            return result;
        }

        public virtual async Task<Result<Guid>> InsertAsync(TDto dto)
        {
            Result<Guid> result = new Result<Guid>();
            var inserted = await _repository.AddEntityAsync(dto);
            if (inserted != Guid.Empty)
            {
                result.Succeeded = true;
                result.Value = inserted;
            }

            return result;
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            Result result = new Result();
            var entityExists = await EntityExists(id);
            if (!entityExists)
            {
                result.Errors.Add(Errors.NotFound, $"{typeof(TDto).Name} not found");
                return result;
            }

            var deletedRows = await _repository.RemoveEntityAsync(id);
            if (deletedRows != default)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public virtual async Task<Result> UpdateAsync(TDto dto)
        {
            Result result = new Result();
            var entityExists = await EntityExists(dto.Id);
            if (!entityExists)
            {
                result.Errors.Add(Errors.NotFound, $"{typeof(TDto).Name} not found");

                return result;
            }

            int updatedRows = await _repository.UpdateEntityAsync(dto);
            if (updatedRows != default)
            {
                result.Succeeded = true;
            }

            return result;
        }
    }
}