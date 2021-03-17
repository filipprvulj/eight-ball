using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.ServiceInterfaces
{
    public interface IReservationService : IBaseService<ReservationDto>
    {
        public Task<Result<List<ReservationDto>>> GetReservationsByUserIdAsync(Guid id);
    }
}