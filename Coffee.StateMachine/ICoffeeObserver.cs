using Coffee.StateMachine.States;

namespace Coffee.StateMachine
{
    public interface ICoffeeObserver
    {
        void StateChanged(State state);
    }
}