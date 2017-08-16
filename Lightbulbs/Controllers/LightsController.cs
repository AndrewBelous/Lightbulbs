using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lightbulbs.Controllers
{
    public class LightsController : ApiController
    {
        private const string PROBLEM_TEXT = @"
There are 100 light bulbs lined up in a row in a long room. Each bulb has its own switch and is currently switched off. The room has an entry door and an exit door. There are 100 people lined up outside the entry door. Each bulb is numbered consecutively from 1 to 100. So is each person.
Person No. 1 enters the room, switches on every bulb, and exits. Person No. 2 enters and flips the switch on every second bulb (turning off bulbs 2, 4, 6, ...). Person No. 3 enters and flips the switch on every third bulb (changing the state on bulbs 3, 6, 9, ...). This continues until all 100 people have passed through the room.
How many of the light bulbs are illuminated after the 100th person has passed through the room? More specifically, which light bulbs are still illuminated, and why?
        ";

        public string Get()
        {
            return PROBLEM_TEXT;
        }

        public LightSession Get(Guid id)
        {
            return LightBulbManager.Self.GetSession(id);
        }

        public LightResponse Post(LightRequest request)
        {
            var response = LightBulbManager.Self.ToggleLights(request);
            
            return response;
        }
    }   //c
}
