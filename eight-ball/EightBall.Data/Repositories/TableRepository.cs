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
    public class TableRepository : BaseRepository<Table, TableDto>, ITableRepository
    {
        public TableRepository(EightBallDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<TableDto> GetByIdAsync(Guid id)
        {
            Task<Table> table = Entities.Include(a => a.Appointments).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<TableDto>(await table);
        }

        public async Task<int> AddTableAppointmentAsync(Guid id, AppointmentDto appointmentDto)
        {
            Table table = _mapper.Map<Table>(await GetByIdAsync(id));
            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);

            table.Appointments = new List<Appointment>() { appointment };
            Entities.Update(table);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTableUniqueAsync(string name)
        {
            bool tableExists = await Entities.AnyAsync(t => t.Name == name);
            return !tableExists;
        }

        public async Task<bool> IsTableUniqueAsync(string name, Guid id)
        {
            bool tableExists = await Entities.AnyAsync(t => t.Name == name && t.Id != id);
            return !tableExists;
        }
    }
}