using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbulbs
{
    internal class LightBulbManager
    {
        Dictionary<Guid, LightSession> _sessions = new Dictionary<Guid, LightSession>();

        public LightSession GetSession(Guid id)
        {
            if (_sessions.ContainsKey(id))
                return _sessions[id];
            else
                return null;
        }

        public LightResponse ToggleLights(LightRequest request)
        {
            bool[] lights = new bool[request.NumberOfLights];

            var sessionId = Guid.NewGuid();
            var session = new LightSession()
            {
                Results = lights,
                SessionId = sessionId,
                Steps = new List<bool[]>()
            };
            
            for (int i = 1; i <= request.PeopleThatMatter; i++)
            {
                session.Steps.Add((bool[])lights.Clone());
                ToggleLights(ref lights, i - 1);
            }

            _sessions.Add(sessionId, session);

            return (LightResponse)session;
        }

        private void ToggleLights(ref bool[] lights, int skipToLight)
        {
            for (int i = skipToLight; i < lights.Length; i += skipToLight + 1)
            {
                lights[i] = !lights[i];
            }
        }

        #region Singleton Implementation
        /// <summary>
        /// Singleton implementation per John Skeet:
        /// http://csharpindepth.com/Articles/General/Singleton.aspx
        /// Class allowed to be instantiated in order to accomodate unit testing
        /// </summary>
        private static readonly Lazy<LightBulbManager> _lazy =
                new Lazy<LightBulbManager>(() => new LightBulbManager());
        public static LightBulbManager Self { get { return _lazy.Value; } }
        #endregion
    }   //c
}
