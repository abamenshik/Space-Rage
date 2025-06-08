namespace MyCheats
{
    public abstract class CheatBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public void Do()
        {
            DoProt();

            UnityEngine.Debug.Log($"Cheat {GetType().Name} is applied!");
        }
        protected abstract void DoProt();
    }
}