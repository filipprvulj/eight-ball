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