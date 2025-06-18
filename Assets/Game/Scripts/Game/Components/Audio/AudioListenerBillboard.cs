using UnityEngine;

// скрипт, чтобы AudioListener корректно определял сторону звука
// актуально для игр от 3го лица
// подробнее: https://www.youtube.com/watch?v=ghnXK5X3E-g
// тайминг 3:48, видос впринципе короткий, можно фул гляянуть

public class AudioListenerBillboard : MonoBehaviour
{
    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}