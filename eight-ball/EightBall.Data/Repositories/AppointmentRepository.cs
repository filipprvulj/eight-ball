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
    public class AppointmentRepository : BaseRepository<Appointment, AppointmentDto>, IAppointmentRepository
    {
        public AppointmentRepository(EightBallDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<AppointmentDto> GetByIdAsync(Guid id)
        {
            Appointment appointment = await Entities
                .Include(a => a.Tables
                .Where(t => !_context.Reservations.Any(r => r.AppointmentId == id && r.TableId == t.Id)))
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<bool> IsAppointmentUnique(AppointmentDto appointmentDto)
        {
            bool entityExists = await Entities.AnyAsync(a => a.Start == appointmentDto.Start && a.End == appointmentDto.End && a.Id != appointmentDto.Id);
            return !entityExists;
        }
    }
}