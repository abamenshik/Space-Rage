using UnityEngine;

namespace SpaceRage
{
    public class Arms : MonoBehaviour
    {
        [SerializeField] private Sprite armBusy;
        [SerializeField] private Sprite armFree;
        [SerializeField] private float maxZ;
        [SerializeField, Min(0)] private float maxZatSpeed;
        [SerializeField, Range(0f, 1f)] private float smoothness;

        private Rigidbody rb;
        private SpriteRenderer[] renderers;

        private void Awake()
        {
            renderers = GetComponentsInChildren<SpriteRenderer>();

            var player = FindFirstObjectByType<PlayerController>();
            rb = player.GetComponent<Rigidbody>();

            player.OnBrakingStateChange += Player_OnBrakingStateChange;
        }

        private void Player_OnBrakingStateChange(bool isBraking)
        {
            foreach (var renderer in renderers)
            {
                renderer.sprite = isBraking ? armBusy : armFree;
            }
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
