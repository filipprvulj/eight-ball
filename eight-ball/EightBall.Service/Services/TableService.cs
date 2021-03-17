using EightBall.Shared;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Service.Services
{
    public class TableService : BaseService<TableDto, ITableRepository>, ITableService
    {
        private readonly IAppointmentService _appointmentService;

        public TableService(ITableRepository repository, IAppointmentService appointmentService) : base(repository)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Result> AddTableAppointmentAsync(Guid id, Guid appointmentId)
        {
            Result result = new Result();
            var tableResult = await GetByIdAsync(id);
            if (!tableResult.Succeeded)
            {
                result.Errors = tableResult.Errors;
                return result;
            }

            var appointmentResult = await _appointmentService.GetByIdAsync(appointmentId);
            if (!appointmentResult.Succeeded)
            {
                result.Errors = appointmentResult.Errors;
                return result;
            }

            if (tableResult.Value.Appointments.Any(a => a.Id == appointmentId))
            {
                result.Errors.Add("AppointmentId", "Ovaj termin već postoji");
                return result;
            }

            if (tableResult.Value.Appointments.Any(a => a.Start <= appointmentResult.Value.End && appointmentResult.Value.Start <= a.End))
            {
                result.Errors.Add("AppointmentId", "Termin se preklapa sa već postojećim");
                return result;
            }

            var appointmentsAdded = await _repository.AddTableAppointmentAsync(id, appointmentResult.Value);
            if (appointmentsAdded != default)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public override async Task<Result<Guid>> InsertAsync(TableDto dto)
        {
            Result<Guid> result = new Result<Guid>();
            bool isTableUnique = await _repository.IsTableUniqueAsync(dto.Name);
            if (!isTableUnique)
            {
                result.Errors.Add("Name", "Sto sa ovim nazivom već postoji");
            }
            else
            {
                result = await base.InsertAsync(dto);
            }

            return result;
        }

        public async Task<Result> RemoveTableAppointmentAsync(Guid id, Guid appointmentId)
        {
            Result result = new Result();
            bool tableExists = await _repository.EntityExists(id);
            if (!tableExists)
            {
                result.Errors.Add(Errors.NotFound, "Table not found");
                return result;
            }

            var appointmentExists = await _appointmentService.EntityExists(appointmentId);
            if (!appointmentExists)
            {
                result.Errors.Add(Errors.NotFound, "Appointment not found");
                return result;
            }

            int rowsDeleted = await _repository.RemoveTableAppointmentAsync(id, appointmentId);
            if (rowsDeleted == default)
            {
                result.Errors.Add("TableAppointment", "Remove failed");
                return result;
            }

            result.Succeeded = true;
            return result;
        }

        public override async Task<Result> UpdateAsync(TableDto dto)
        {
            Result result = new Result();
            bool entityExists = await _repository.EntityExists(dto.Id);
            if (!entityExists)
            {
                result.Errors.Add(Errors.NotFound, "Table not found");
            }

            bool isTableUnique = await _repository.IsTableUniqueAsync(dto.Name, dto.Id);
            if (!isTableUnique)
            {
                result.Errors.Add("Name", "Sto sa ovim nazivom već postoji");
            }
            else
            {
                result = await base.UpdateAsync(dto);
            }

            return result;
        }
    }
}