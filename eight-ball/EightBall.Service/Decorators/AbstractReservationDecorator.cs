using EightBall.Shared;
using EightBall.Shared.Dtos;
using EightBall.Shared.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Service.Decorators
{
    public abstract class AbstractReservationDecorator : IReservationService
    {
        private readonly IReservationService _reservationService;

        protected AbstractReservationDecorator(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public Task<bool> EntityExists(Guid id)
        {
            return _reservationService.EntityExists(id);
        }

        public Task<Result<ReservationDto>> GetByIdAsync(Guid id)
        {
            return _reservationService.GetByIdAsync(id);
        }

        public Task<Result<List<ReservationDto>>> GetEntitiesAsync()
        {
            return _reservationService.GetEntitiesAsync();
        }

        public Task<Result<List<ReservationDto>>> GetReservationsByUserIdAsync(Guid id)
        {
            return _reservationService.GetReservationsByUserIdAsync(id);
        }

        public virtual Task<Result<Guid>> InsertAsync(ReservationDto dto)
        {
            return _reservationService.InsertAsync(dto);
        }

        public virtual Task<Result> RemoveAsync(Guid id, Guid currentUserId)
        {
            return _reservationService.RemoveAsync(id, currentUserId);
        }

        public Task<Result> RemoveAsync(Guid id)
        {
            return _reservationService.RemoveAsync(id);
        }

        public Task<Result> UpdateAsync(ReservationDto dto)
        {
            return _reservationService.UpdateAsync(dto);
        }
    }
}