using System.Collections;

namespace MyCheats
{
    public abstract class CheatBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public IEnumerator Do()
        {
            UnityEngine.Debug.Log($"Cheat {GetType().Name} is applied!");

            yield return DoProt();
        }
        protected abstract IEnumerator DoProt();
    }
}