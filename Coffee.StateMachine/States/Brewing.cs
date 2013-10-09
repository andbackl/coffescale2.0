using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Brewing : State
    {
        private long _timeRemaining;

        public Brewing(int weight, ICoffeeObserver observer)
            : base(weight, observer)
        {
            _timeRemaining = Configuration.BrewingTime;
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
                TimeRemaining = _timeRemaining
            };
        }
    }
}