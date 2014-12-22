using System.ComponentModel.Composition;
using Framework.Core;

namespace ImportOrderingTest
{
    public class TestTasks
    {
        [Export(ApplicationBootstrapper.STARTUP_TASK_NAME, typeof (BootstrapperTask))]
        [ExportMetadata("Order", 1)]
        public void Task1()
        {

        }
    
        [Export(ApplicationBootstrapper.STARTUP_TASK_NAME, typeof(BootstrapperTask))]
        public void TaskLast()
        {

        }
    }
}
