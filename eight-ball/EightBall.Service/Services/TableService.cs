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
        public TableService(ITableRepository repository) : base(repository)
        {
        }

        public override async Task<Result<Guid>> InsertAsync(TableDto dto)
        {
            Result<Guid> result = new Result<Guid>();
            bool isTableUnique = await _repository.IsTableUniqueAsync(dto.Name);
            if (isTableUnique)
            {
                result.Errors.Add("Name", "Sto sa ovim nazivom već postoji");
            }
            else
            {
                result = await base.InsertAsync(dto);
            }

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