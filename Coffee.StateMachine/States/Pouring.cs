using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Pouring : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight > Configuration.WeightWithFilterAndEmptyCanMin)
            {
                Debug.WriteLine("Can returned, changing state.");
                NextState = new NotFull();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}