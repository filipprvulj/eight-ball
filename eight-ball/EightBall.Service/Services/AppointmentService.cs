using EightBall.Service.Services;
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
    public class AppointmentService : BaseService<AppointmentDto, IAppointmentRepository>, IAppointmentService
    {
        public AppointmentService(IAppointmentRepository repository) : base(repository)
        {
        }

        public override async Task<Result<Guid>> InsertAsync(AppointmentDto dto)
        {
            Result<Guid> result = new Result<Guid>();
            if (!IsDateValid(dto))
            {
                result.Errors.Add("Termin", "Uneto vreme nije validno");
                return result;
            }

            bool isAppointmentUnique = await _repository.IsAppointmentUnique(dto);
            if (!isAppointmentUnique)
            {
                result.Errors.Add("Termin", "Ovaj termin već postoji");
            }
            else
            {
                result = await base.InsertAsync(dto);
            }
            return result;
        }

        public override async Task<Result> UpdateAsync(AppointmentDto dto)
        {
            Result result = new Result();
            if (!IsDateValid(dto))
            {
                result.Errors.Add("Termin", "Uneto vreme nije validno");
                return result;
            }

            bool entityExists = await _repository.EntityExists(dto.Id);
            if (!entityExists)
            {
                result.Errors.Add(Errors.NotFound, "Appointment not found");
            }

            bool isAppointmentUnique = await _repository.IsAppointmentUnique(dto);
            if (!isAppointmentUnique)
            {
                result.Errors.Add("Termin", "Ovaj termin već postoji");
            }
            else
            {
                result = await base.UpdateAsync(dto);
            }

            return result;
        }

        private bool IsDateValid(AppointmentDto dto)
        {
            dto.Start.AddSeconds(-1 * dto.Start.Second);
            dto.End.AddSeconds(-1 * dto.End.Second);
            var currentTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            bool isDateValid = dto.Start < dto.End;
            if (dto.Id == default)
            {
                isDateValid = isDateValid && dto.Start >= currentTime;
            }

            return isDateValid;
        }
    }
}