using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace Lightbulbs.Hosting
{
    public class WebApiLoader : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> defaultAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(defaultAssemblies);

            Type t = typeof(Controllers.LightsController);
            Assembly a = t.Assembly;
            defaultAssemblies.Add(a);

            return assemblies;
        }
    }   //c
}
