using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Scene
{
    public class ReloadScene : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
