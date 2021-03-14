using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Shared
{
    public class Result
    {
        public bool Succeeded { get; set; } = false;
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }

    public class Result<T>
    {
        public bool Succeeded { get; set; } = false;
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public T Value { get; set; }
    }
}