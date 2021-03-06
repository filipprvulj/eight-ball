using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.RepositoryInterfaces
{
    public interface IAppointmentRepository : IBaseRepository<AppointmentDto>
    {
        public Task<bool> IsAppointmentUnique(AppointmentDto appointmentDto);
    }
}