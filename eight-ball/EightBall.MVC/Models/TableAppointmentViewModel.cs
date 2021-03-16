using EightBall.Shared.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Models
{
    public class TableAppointmentViewModel
    {
        public Guid TableId { get; set; }
        public Guid AppointmentId { get; set; }
        public List<SelectListItem> Appointments { get; set; } = new List<SelectListItem>();
    }
}