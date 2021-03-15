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

        public async Task<bool> IsAppointmentUnique(AppointmentDto appointmentDto)
        {
            bool entityExists = await Entities.AnyAsync(a => a.Start == appointmentDto.Start && a.End == appointmentDto.End && a.Id != appointmentDto.Id);
            return !entityExists;
        }
    }
}