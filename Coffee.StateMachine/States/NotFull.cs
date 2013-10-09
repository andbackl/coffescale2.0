using System;
using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class NotFull : State
    {
        public NotFull(int weight, ICoffeeObserver observer)
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
            else if (Weight >= Configuration.WeightWithFilterAndEmptyCanMin && Weight <= Configuration.WeightWithFilterAndEmptyCanMax)
            {
                Debug.WriteLine("Empty weight reached, changing state.");
                NextState = new Empty(Weight, Observer);
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