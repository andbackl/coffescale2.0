using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Empty : State
    {
        public Empty(int weight, ICoffeeObserver observer) : base(weight, observer)
        {
        }

        public override void Update(long elapsedTimeMillis)
        {
            if (Weight >= Configuration.WeightWithFilterWithoutCanMin && Weight < Configuration.WeightWithFilterWithoutCanMax)
            {
                Debug.WriteLine("Under empty weight reached, changing state.");
                NextState = new Preparing(Weight, Observer);
            }
            base.Update(elapsedTimeMillis);
        }

        public override object GetValues()
        {
            return new
            {
                StateName,
                ElapsedTime,
                Weight,
                NumberOfCups = GetNumberOfCups()
            };
        }
    }
}