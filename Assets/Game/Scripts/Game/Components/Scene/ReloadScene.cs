using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Scene
{
    public class ReloadScene : MonoBehaviour
    {
        public KeyCode reloadKeyCode = KeyCode.R;

        private void Update()
        {
            if (Input.GetKeyDown(reloadKeyCode))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
