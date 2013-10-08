using System.Diagnostics;

namespace Coffee.StateMachine.States
{
    public class Preparing : State
    {
        public override void Update(long elapsedTimeMillis)
        {
            if (Weight >= Configuration.WeightWithFilterAndFullCanMin && Weight < Configuration.WeightWithFilterAndFullCanMax)
            {
                Debug.WriteLine("Max weight reached, changing state.");
                NextState = new Brewing();
            }
            base.Update(elapsedTimeMillis);
        }
    }
}