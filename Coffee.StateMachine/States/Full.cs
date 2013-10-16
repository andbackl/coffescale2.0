using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Full : State
    {
        public Full(int weight, ICoffeeObserver observer)
            : base(weight, observer)
        {
        }

        public override void Update(long elapsedTimeMillis)
        {
            if (Weight >= Configuration.WeightWithFilterWithoutCanMin && Weight < Configuration.WeightWithFilterWithoutCanMax)
            {
                Debug.WriteLine("Can removed, changing state.");
                NextState = new Pouring(Weight, Observer);
            }
            else if (Weight < Configuration.WeightWithFilterAndFullCan)
            {
                Debug.WriteLine("Not max weight, changing state.");
                NextState = new NotFull(Weight, Observer);
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
                NumberOfCups = 10
            };
        }
    }
}