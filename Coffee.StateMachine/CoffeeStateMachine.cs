using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Coffee.StateMachine.States;

namespace Coffee.StateMachine
{
    public class CoffeeStateMachine
    {
        private State _currentState;
        private int _weight = Configuration.WeightWithFilterAndEmptyCan;
        private readonly Queue<int> _weightHistory = new Queue<int>();

        public CoffeeStateMachine(ICoffeeObserver observer)
        {
            _currentState = new Empty(_weight, observer);
            Active = true;
            Task.Factory.StartNew(Run);
        }

        public void Update(int weight)
        {
            Debug.WriteLine("Update with weight " + weight);
            _weight = weight;
            if (_weightHistory.Count > Configuration.MaxHistorySize)
            {
                _weightHistory.Dequeue();
            }
            _weightHistory.Enqueue(weight);
        }

        private void Run()
        {
            var timer = Stopwatch.StartNew();
            while (Active)
            {
                _currentState.Update(timer.ElapsedMilliseconds);
                _currentState = _currentState.NextState;
                _currentState.Weight = _weight;
                _currentState.WeightHistory = _weightHistory;
                timer.Restart();
                Thread.Sleep(500);
            }
        }

        public bool Active { get; set; }
    }
}
