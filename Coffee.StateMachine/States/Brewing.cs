using System;
using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Brewing : State
    {
        private long _timeRemaining = Configuration.BrewingTime;

        public Brewing(int weight, ICoffeeObserver observer)
            : base(weight, observer)
        {
        }

        public override void Update(long elapsedTimeMillis)
        {
            _timeRemaining -= elapsedTimeMillis;
            if (_timeRemaining <= 0)
            {
                Debug.WriteLine("Time limit reached, changing state.");
                NextState = new Full(Weight, Observer);
            }
            base.Update(elapsedTimeMillis);
        }

        public override object GetValues()
        {
            return new
            {
                StateName = GetType().Name,
                ElapsedTime,
                Weight,
                TimeRemaining = _timeRemaining,
                BrewingDone = GetBrewingDoneTime()
            };
        }

        private long GetBrewingDoneTime()
        {
            return (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                   .TotalMilliseconds + _timeRemaining);
        }
    }
}