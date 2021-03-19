using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.RepositoryInterfaces
{
    public interface IReservationRepository : IBaseRepository<ReservationDto>
    {
        public Task<bool> EntityExists(Guid appointmentId, Guid tableId);

        public Task<List<ReservationDto>> GetReservationsByUserIdAsync(Guid id);
    }
}