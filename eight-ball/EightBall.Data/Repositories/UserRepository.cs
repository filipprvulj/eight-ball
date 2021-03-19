using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.Strings;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> GetEmployeeIdsAsync()
        {
            var employees = await _userManager.GetUsersInRoleAsync(RoleNames.Employee);

            return employees.Select(e => e.Id).ToList();
        }
    }
}