using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Framework.Core;

namespace ImportOrderingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var lib_catalog = new AssemblyCatalog(typeof(BootstrapperTask).Assembly);
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            var aggregate_catalog = new AggregateCatalog(lib_catalog, catalog);
            var container = new CompositionContainer(aggregate_catalog);

            var tasks = container.GetExports<BootstrapperTask, IOrderMetadata>(ApplicationBootstrapper.STARTUP_TASK_NAME)
                                 .OrderBy(l => l.Metadata.Order)
                                 .ToList();

            foreach (var task in tasks)
            {
                Console.WriteLine(task.Metadata.Order);
            }
        }
    }
}
