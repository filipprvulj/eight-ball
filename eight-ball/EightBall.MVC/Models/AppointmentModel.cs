using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Models
{
    public class AppointmentModel : BaseModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}