using System.Collections.Generic;

namespace Coffee.StateMachine.States
{
    public abstract class State
    {
        public int Weight { get; set; }
        public State NextState { get; protected set; }
        public Queue<int> WeightHistory { get; set; }
        public long ElapsedTime { get; private set; }

        protected State()
        {
            NextState = this;
            NotificationService.OnStateChanged(this);
        }

        public virtual void Update(long elapsedTimeMillis)
        {
            ElapsedTime += elapsedTimeMillis;
        }
    }
}