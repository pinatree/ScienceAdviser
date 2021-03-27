using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;

namespace ScienceAdviser.DI
{
    public static class DIContainer
    {
        public static IContainer Container;

        public static T GetInstance<T>()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
