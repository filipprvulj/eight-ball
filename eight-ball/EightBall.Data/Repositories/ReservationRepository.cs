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

        public Task<List<ReservationDto>> GetReservationsByUserIdAsync(Guid id)
        {
            var reservations = Entities.Where(r => r.UserId == id.ToString());
            return _mapper.ProjectTo<ReservationDto>(reservations).ToListAsync();
        }
    }
}