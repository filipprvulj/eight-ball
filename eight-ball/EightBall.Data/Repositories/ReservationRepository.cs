using AutoMapper;
using EightBall.Data.Entities;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Data.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation, ReservationDto>, IReservationRepository
    {
        public ReservationRepository(EightBallDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<bool> EntityExists(Guid appointmentId, Guid tableId)
        {
            return Entities.AnyAsync(r => r.AppointmentId == appointmentId && r.TableId == tableId);
        }

        public override async Task<ReservationDto> GetByIdAsync(Guid id)
        {
            Task<Reservation> reservation = Entities
                .Include(r => r.Appointment)
                .Include(r => r.Table)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<ReservationDto>(await reservation);
        }

        public Task<List<ReservationDto>> GetReservationsByUserIdAsync(Guid id)
        {
            var reservations = Entities.Where(r => r.UserId == id.ToString());
            return _mapper.ProjectTo<ReservationDto>(reservations).ToListAsync();
        }
    }
}