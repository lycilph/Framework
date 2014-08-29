using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
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
            return assemblies;
        }

        protected override object GetInstance(Type service_type, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service_type) : key;
            var exports = container.GetExportedValues<object>(contract).ToList();

            if (exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service_type)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(service_type));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            logger.Trace("Startup");

            RunTasks(STARTUP_TASK_NAME);
            DisplayRootViewFor<IShell>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            logger.Trace("Exit");

            base.OnExit(sender, e);
            RunTasks(SHUTDOWN_TASK_NAME);
        }

        protected virtual void CurrentOnSessionEnding(object sender, SessionEndingCancelEventArgs session_ending_cancel_event_args)
        {
            logger.Trace("Session Ending");

            RunTasks(SHUTDOWN_TASK_NAME);
        }

        protected virtual void RunTasks(string contract)
        {
            container.GetExportedValues<BootstrapperTask>(contract)
                     .Apply(t => t());
        }
    }
}
