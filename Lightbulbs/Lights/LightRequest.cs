using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbulbs
{
    public class LightRequest
    {
        public int NumberOfLights { get; set; }
        public int NumberOfPeople { get; set; }

        public int PeopleThatMatter 
        {
            get
            {
                return (NumberOfPeople > NumberOfLights ? NumberOfLights + 1 : NumberOfPeople);
            }
        }
    }   //c
}
