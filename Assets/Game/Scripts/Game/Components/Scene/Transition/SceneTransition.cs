using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay.Components.Scenes
{
    public class SceneTransition : MonoBehaviour
    {
        private AsyncOperation loadingSceneOperation;
        private SceneTransitionEffectBase effect;

        private void Awake()
        {
            effect = GetComponent<SceneTransitionEffectBase>();
        }
        private IEnumerator Start()
        {
            effect.ResetEffect();

            yield return effect.Open();
            OnAnimationOver();
        }

        public void SwitchScene(string sceneName)
        {
            StartCoroutine(CloseView());

            loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

            loadingSceneOperation.allowSceneActivation = false;
        }
        private IEnumerator CloseView()
        {
            yield return effect.Close();
            OnAnimationOver();
        }
        public void OnAnimationOver()
        {
            if (loadingSceneOperation != null)
                loadingSceneOperation.allowSceneActivation = true;
        }
    }
}