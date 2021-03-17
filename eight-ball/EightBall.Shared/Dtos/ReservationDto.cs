using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.Dtos
{
    public class ReservationDto : BaseDto
    {
        public Guid TableId { get; set; }
        public TableDto Table { get; set; }
        public Guid AppointmentId { get; set; }
        public AppointmentDto Appointment { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}