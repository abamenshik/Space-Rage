using UnityEngine;

namespace SpaceRage.Gameplay.Entity.Player.Debug
{
    [RequireComponent(typeof(LineRenderer))]
    public class ImpulseLine : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private PlayerController player;

        void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        private void LateUpdate()
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * player.impulseCast.distance);

            if (player.impulseCast.TryRayCast(player.transform.forward, out _))
            {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            else
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
        }
    }
}