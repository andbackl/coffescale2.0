using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Coffee.WorkerRole.Workers.LogChangesToTableStorage;
using Coffee.WorkerRole.Workers.StateMachinePushToWeb;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Coffee.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly IEnumerable<IWorker> _workers;
        private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);

        public WorkerRole()
        {
            _workers = new IWorker[]
            {
                new LogChangesToTableStorageWorker(),                
                new StateMachineWorker()
            };
        }

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            foreach (var worker in _workers)
                worker.Run();

            _completedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            foreach (var worker in _workers)
                worker.OnStart();

            return base.OnStart();
        }

        public override void OnStop()
        {
            _completedEvent.Set();

            foreach (var worker in _workers)
                worker.OnStop();

            base.OnStop();
        }
    }
}
