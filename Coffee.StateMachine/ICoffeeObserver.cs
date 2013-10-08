using Coffee.StateMachine.States;

namespace Coffee.StateMachine
{
    public interface ICoffeeObserver
    {
        void Tick(State state);        
    }
}