using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.Dtos
{
    public class AppointmentDto : BaseDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<TableDto> Tables { get; set; }
    }
}