using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public Guid TableId { get; set; }
        public Table Table { get; set; }
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}