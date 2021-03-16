using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.RepositoryInterfaces
{
    public interface ITableRepository : IBaseRepository<TableDto>
    {
        public Task<bool> IsTableUniqueAsync(string name);

        public Task<bool> IsTableUniqueAsync(string name, Guid id);

        public Task<int> AddTableAppointmentAsync(Guid id, AppointmentDto appointmentDto);
    }
}