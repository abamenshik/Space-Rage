using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay.Components.Scenes
{
    public static class SwitchScene
    {
        public static void Switch(string sceneName)
        {
            var transition = Object.FindFirstObjectByType<SceneTransition>();

            if (transition != null)
            {
                transition.SwitchScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}