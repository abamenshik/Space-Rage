using UnityEngine;
using UnityEngine.Events;

namespace Components.Collisions
{
    public class KinematicButDynamicAfterCollision : MonoBehaviour
    {
        [SerializeField, Min(1)] private float breakForce = 10;
        [SerializeField] private UnityEvent onBecameDynamic;

        private Rigidbody rb;
        private bool isBroken;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (isBroken || collision.impulse.magnitude < breakForce)
                return;

            isBroken = true;
            rb.isKinematic = false;
            onBecameDynamic?.Invoke();
        }
    }
}