using System;
using UnityEngine;

namespace Components.Colliders
{
    [Serializable]
    public class RayCaster
    {
        public float distance;
        [SerializeField] private LayerMask layerMask;

        public Transform Origin;

        private Vector2 CursorPos => UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public bool TryRayCast(Vector2 origin, Vector2 direction, out RaycastHit hit)
        {
            return Physics.Raycast(origin, direction, out hit, distance, layerMask);
        }
        public bool TryRayCast(Vector2 direction, out RaycastHit hit)
        {
            return TryRayCast(Origin.position, direction, out hit);
        }
        public bool TryRayCastToCursorPosition(Vector2 origin, out RaycastHit hit)
        {
            return TryRayCast(origin, CursorPos, out hit);
        }
        public bool TryRayCastToCursorPosition(out RaycastHit hit)
        {
            return TryRayCast(Origin.position, CursorPos, out hit);
        }
    }
}