using System.Diagnostics;
using Coffee.StateMachine.States;

namespace Coffee.StateMachine
{
    public static class NotificationService
    {
        public static void OnStateChanged(State state)
        {
            Trace.WriteLine("State changed to {0}", state.GetType().Name);
        }
    }
}