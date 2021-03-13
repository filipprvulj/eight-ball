using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.ServiceInterfaces;
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
    }
}