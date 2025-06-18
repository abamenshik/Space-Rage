using UnityEngine;
using UnityEngine.UI;

public class URL_Button : MonoBehaviour
{
    [SerializeField] private string url;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }
}
