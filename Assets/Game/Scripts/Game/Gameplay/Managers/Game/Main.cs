using UnityEngine;

namespace SpaceRage
{
    public class Main : MonoBehaviour
    {
        static bool isInitialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void Init()
        {
            if (isInitialized)
                return;

            GameObject main = new("---  MAIN  ---");
            main.AddComponent<Main>();
            main.transform.parent = DynamicSpawn.DontDestroyOnLoadParent;

            isInitialized = true;
        }

        private void Awake()
        {
            Debug.Log(new string('=', 10));
            Debug.Log("entry point hit");

            GAME_ENTRY_POINT();
        }
        private void OnDestroy()
        {
            isInitialized = false;
        }

        private void GAME_ENTRY_POINT()
        {
            Application.targetFrameRate = 60;
            MyCheats.Cheats.Init();
        }
    }
}
