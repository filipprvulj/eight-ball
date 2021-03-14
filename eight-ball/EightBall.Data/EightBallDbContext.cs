using EightBall.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Data
{
    public class EightBallDbContext : IdentityDbContext<IdentityUser>
    {
        public EightBallDbContext(DbContextOptions<EightBallDbContext> options) : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
    }
}