namespace Coffee.WorkerRole
{
    public interface IWorker
    {
        void Run();
        void OnStart();
        void OnStop();
    }
}