using EightBall.Shared.Dtos;
using EightBall.Shared.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Service.Services
{
    public class BaseService<TDto> : IBaseService<TDto>
        where TDto : BaseDto
    {
        public Task<TDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> InsertAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}