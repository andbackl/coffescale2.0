using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Pouring : State
    {
        public Pouring(int weight, ICoffeeObserver observer)
            : base(weight, observer)
        {
        }
        
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight > Configuration.WeightWithFilterAndEmptyCanMin)
            {
                Debug.WriteLine("Can returned, changing state.");
                NextState = new NotFull(Weight, Observer);
            }
            base.Update(elapsedTimeMillis);
        }
    }
}