using EightBall.Service.Hubs;
using EightBall.Shared;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
using Microsoft.AspNetCore.SignalR;
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

        public override async Task<Result<Guid>> InsertAsync(ReservationDto dto)
        {
            Result<Guid> result = new Result<Guid>();

            bool reservationExists = await _repository.EntityExists(dto.AppointmentId, dto.TableId);
            if (reservationExists)
            {
                result.Errors.Add("Rezervacija", "Rezervacija vec postoji");
                return result;
            }

            Guid insertedRow = await _repository.AddEntityAsync(dto);
            if (insertedRow == Guid.Empty)
            {
                result.Errors.Add("Insert", "Greska prilikom kreiranja rezervacije");
                return result;
            }

            result.Succeeded = true;
            result.Value = insertedRow;
            return result;
        }

        public async Task<Result<List<ReservationDto>>> GetReservationsByUserIdAsync(Guid id)
        {
            Result<List<ReservationDto>> result = new Result<List<ReservationDto>>();
            var reservations = await _repository.GetReservationsByUserIdAsync(id);

            result.Succeeded = true;
            result.Value = reservations;
            return result;
        }

        public async Task<Result> RemoveAsync(Guid id, Guid currentUserId)
        {
            Result result = new Result();

            var reservation = await _repository.GetByIdAsync(id);
            if (reservation == null)
            {
                result.Errors.Add(Errors.NotFound, "Rezervacija nije pronadjena");
                return result;
            }

            if (reservation.UserId != currentUserId)
            {
                result.Errors.Add("Remove", "Nemate dozvolu da otkazete ovu rezervaciju");
                return result;
            }

            int deletedRows = await _repository.RemoveEntityAsync(id);
            if (deletedRows == default)
            {
                result.Errors.Add("Remove", "Greska prilikom otkazivanja");
                return result;
            }

            result.Succeeded = true;
            return result;
        }
    }
}