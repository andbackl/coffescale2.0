using System;
using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Empty : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight <= Configuration.WeightWithFilterWithoutCan)
            {
                Trace.WriteLine("Under empty weight reached, changing state.");
                NextState = new Preparing();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}