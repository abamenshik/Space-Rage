using UnityEngine;

namespace MyCheats
{
    public class InitCheats : MonoBehaviour
    {
        private void Awake()
        {
            Cheats.Init();
        }
    }
}
