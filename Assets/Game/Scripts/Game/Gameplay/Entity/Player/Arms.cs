using UnityEngine;

namespace SpaceRage
{
    public class Arms : MonoBehaviour
    {
        [SerializeField] private float maxZ;
        [SerializeField, Min(0)] private float maxZatSpeed;
        [SerializeField, Range(0f, 1f)] private float smoothness;

        private Rigidbody rb;

        private void Awake()
        {
            rb = FindFirstObjectByType<PlayerController>().GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var lerp = Mathf.InverseLerp(0, maxZatSpeed, rb.linearVelocity.magnitude);
            var z = Mathf.Lerp(0, maxZ, lerp);
            z = Mathf.Lerp(transform.localPosition.z, z, smoothness);

            var newPos = transform.localPosition;
            newPos.z = z;
            transform.localPosition = newPos;
        }
    }
}
