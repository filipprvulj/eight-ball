using EightBall.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Models
{
    public class RemoveTableAppointmentViewModel
    {
        public TableDto Table { get; set; }
        public AppointmentDto Appointment { get; set; }
    }
}