using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Data.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}