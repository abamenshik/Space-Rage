using UnityEngine;

namespace SpaceRage
{
    public class LookAtPlayer : MonoBehaviour
    {
        private Transform target;

        private void Start()
        {
            target = FindFirstObjectByType<PlayerController>().transform;
        }
        private void LateUpdate()
        {
            transform.LookAt(target);
        }
    }
}
