using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.ServiceInterfaces
{
    public interface IBaseService<TDto> where TDto : BaseDto
    {
        public Task<Result<TDto>> GetByIdAsync(Guid id);

        public Task<Result<List<TDto>>> GetEntitiesAsync();

        public Task<Result<Guid>> InsertAsync(TDto dto);

        public Task<Result> RemoveAsync(Guid id);

        public Task<Result> UpdateAsync(TDto dto);

        public Task<bool> EntityExists(Guid id);
    }
}