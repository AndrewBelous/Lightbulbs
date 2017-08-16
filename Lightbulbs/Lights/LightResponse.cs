using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbulbs
{
    public class LightResponse
    {
        public bool[] Results { get; set; }
        public Guid SessionId { get; set; }
        public LightSessionStats Stats { get; set; }
    }   //c
}
