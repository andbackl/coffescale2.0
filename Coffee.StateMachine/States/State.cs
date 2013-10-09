using System.Collections.Generic;

namespace Coffee.StateMachine.States
{
    public abstract class State
    {
        public int Weight { get; set; }
        public State NextState { get; protected set; }
        public Queue<int> WeightHistory { get; set; }
        public long ElapsedTime { get; private set; }
        public ICoffeeObserver Observer { get; private set; }

        public string StateName 
        {
            get { return GetType().Name; }
        }

        protected State(int weight, ICoffeeObserver observer)
        {
            NextState = this;
            Weight = weight;
            Observer = observer;
            Observer.StateChanged(this);
        }

        public virtual void Update(long elapsedTimeMillis)
        {
            ElapsedTime += elapsedTimeMillis;
        }

        public virtual object GetValues()
        {
            return new
            {
                StateName = GetType().Name,
                ElapsedTime,
                Weight
            };
        }

        protected int GetNumberOfCups()
        {
            var antalGramKaffe = Weight - Configuration.WeightWithFilterAndEmptyCan;
            var cups = antalGramKaffe/Configuration.OneCupWeight;
            return (int)(cups + 0.5f);
        }
    }
}