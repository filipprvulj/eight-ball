using EightBall.Shared;
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
    public class ReservationService : BaseService<ReservationDto, IReservationRepository>, IReservationService
    {
        public ReservationService(IReservationRepository repository) : base(repository)
        {
        }

        public async Task<Result<List<ReservationDto>>> GetReservationsByUserIdAsync(Guid id)
        {
            Result<List<ReservationDto>> result = new Result<List<ReservationDto>>();
            var reservations = await _repository.GetReservationsByUserIdAsync(id);

            result.Succeeded = true;
            result.Value = reservations;
            return result;
        }
    }
}