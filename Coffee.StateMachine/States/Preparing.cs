using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Preparing : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight >= Configuration.WeightWithFilterAndFullCan)
            {
                Trace.WriteLine("Max weight reached, changing state.");
                NextState = new Brewing();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}