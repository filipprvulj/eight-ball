using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.ServiceInterfaces
{
    public interface ITableService : IBaseService<TableDto>
    {
        public Task<Result> AddTableAppointmentAsync(Guid id, Guid appointmentId);

        public Task<Result> RemoveTableAppointmentAsync(Guid id, Guid appointmentId);
    }
}