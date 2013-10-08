using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Coffee.StateMachine.States;

namespace Coffee.StateMachine
{
    public class CoffeStateMachine
    {
        private State _currentState = new Empty();
        private int _weight = Configuration.WeightWithFilterAndEmptyCan;
        private Queue<int> WeightHistory = new Queue<int>();

        public CoffeStateMachine()
        {
            
            _weight = Configuration.WeightWithFilterAndEmptyCan;
            Active = true;
            Task.Factory.StartNew(Run);
        }

        public void Update(int weight)
        {
            _weight = weight;
            if (WeightHistory.Count > Configuration.MaxHistorySize)
            {
                WeightHistory.Dequeue();
            }
            WeightHistory.Enqueue(weight);
        }

        private void Run()
        {
            var timer = Stopwatch.StartNew();
            while (Active)
            {
                _currentState.Update(timer.ElapsedMilliseconds);
                _currentState = _currentState.NextState;
                _currentState.Weight = _weight;
                _currentState.WeightHistory = WeightHistory;
                timer.Restart();
                Thread.Sleep(500);
            }
        }

        public bool Active { get; set; }
    }
}
