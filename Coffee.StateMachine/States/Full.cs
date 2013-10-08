using System;
using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Full : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight < Configuration.WeightWithFilterAndFullCan)
            {
                Trace.WriteLine("Not max weight, changing state.");
                NextState = new NotFull();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}