using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class NotFull : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight >= Configuration.WeightWithFilterWithoutCanMin && Weight < Configuration.WeightWithFilterWithoutCanMax)
            {
                Trace.WriteLine("Can removed, changing state.");
                NextState = new Pouring();
            }
            else if (Weight >= Configuration.WeightWithFilterAndEmptyCanMin && Weight <= Configuration.WeightWithFilterAndEmptyCanMax)
            {
                Trace.WriteLine("Empty weight reached, changing state.");
                NextState = new Empty();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}