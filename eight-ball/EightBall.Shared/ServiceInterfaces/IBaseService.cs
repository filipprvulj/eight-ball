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
        public Task<TDto> GetByIdAsync(Guid id);

        public Task<List<TDto>> GetEntitiesAsync();

        public Task<Guid> InsertAsync(TDto dto);

        public Task<int> RemoveAsync(Guid id);

        public Task<int> UpdateAsync(TDto dto);
    }
}