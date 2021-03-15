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
    }
}