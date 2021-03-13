using AutoMapper;
using EightBall.Data.Entities;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
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
    }
}