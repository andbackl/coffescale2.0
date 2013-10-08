using System;
using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Pouring : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight > Configuration.WeightWithFilterAndEmptyCan)
            {
                Trace.WriteLine("Can returned, changing state.");
                NextState = new NotFull();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}