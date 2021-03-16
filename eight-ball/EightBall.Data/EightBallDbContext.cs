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

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Table>()
                .HasMany(l => l.Appointments)
                .WithMany(r => r.Tables)
                .UsingEntity(join => join.ToTable("TableAppointments"));
        }
    }
}