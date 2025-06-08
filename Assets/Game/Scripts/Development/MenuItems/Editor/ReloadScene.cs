using UnityEditor;
using UnityEngine.SceneManagement;

public class ReloadScene
{
    [MenuItem("Tools/ReloadScene %l")]
    private static void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}