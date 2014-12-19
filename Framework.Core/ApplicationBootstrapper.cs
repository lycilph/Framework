using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Framework.Core.Shell;
using NLog;
using LogManager = Caliburn.Micro.LogManager;

namespace Framework.Core
{
    public class ApplicationBootstrapper : BootstrapperBase
    {
        private static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CompositionContainer container;

        public const string STARTUP_TASK_NAME = "Startup";
        public const string SHUTDOWN_TASK_NAME = "Shutdown";

        static ApplicationBootstrapper()
        {
            LogManager.GetLog = type => new DebugLog(type);
        }

        public ApplicationBootstrapper()
        {
            logger.Trace("Created");
            Initialize();
            Application.Current.SessionEnding += CurrentOnSessionEnding;
        }

        protected override void Configure()
        {
            var catalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)));
            container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(Assembly.GetEntryAssembly());

            logger.Trace("SelectAssemblies");
            assemblies.Apply(a => logger.Trace("Found assembly: " + a.FullName));

            return assemblies;
        }

        protected override object GetInstance(Type service_type, string key)
        {
            logger.Trace("GetInstance of {0} for key {1}", service_type.FullName, key);

            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service_type) : key;
            var exports = container.GetExportedValues<object>(contract).ToList();

            if (exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service_type)
        {
            logger.Trace("GetAllInstances of {0}", service_type.FullName);

            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(service_type));
        }

        protected override void BuildUp(object instance)
        {
            logger.Trace("BuildUp of {0}", instance.GetType().FullName);

            container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            logger.Trace("Startup");

            logger.Trace("Running startup tasks");
            RunTasks(STARTUP_TASK_NAME);
            DisplayRootViewFor<IShell>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            logger.Trace("Running shutdown tasks");
            RunTasks(SHUTDOWN_TASK_NAME);

            logger.Trace("Exit");

        }

        protected virtual void CurrentOnSessionEnding(object sender, SessionEndingCancelEventArgs session_ending_cancel_event_args)
        {
            logger.Trace("Running shutdown tasks");
            RunTasks(SHUTDOWN_TASK_NAME);
            
            logger.Trace("Session Ended");
        }

        protected virtual void RunTasks(string contract)
        {
            container.GetExportedValues<BootstrapperTask>(contract)
                     .Apply(t => t());
        }
    }
}
