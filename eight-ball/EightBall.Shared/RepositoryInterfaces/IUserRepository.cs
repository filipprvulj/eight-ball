using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<List<string>> GetEmployeeIdsAsync();
    }
}