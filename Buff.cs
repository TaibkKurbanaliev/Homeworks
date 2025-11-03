namespace Homework1
{
    public class Buff
    {
        public event Action<Buff> Finished;

        public int Value {  get; private set; }
        public int TimesToApplying { get; private set; }

        public Buff(int value, int timesToApplying)
        {
            Value = value;
            TimesToApplying = timesToApplying;
        }

        public int ApplyBuff(int value)
        {
            return Value + value;
        }

        public void DecreaseBuff()
        {
            TimesToApplying--;
        }
    }
}
