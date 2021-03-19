using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared.Dtos
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}