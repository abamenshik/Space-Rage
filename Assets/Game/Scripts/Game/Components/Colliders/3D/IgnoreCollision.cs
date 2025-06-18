using UnityEngine;

namespace Components.Colliders
{
    public class IgnoreCollision : MonoBehaviour
    {
        [SerializeField] private Collider[] colliders;

        private void Start()
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                for (int j = 0; j < colliders.Length; j++)
                {
                    Physics.IgnoreCollision(colliders[i], colliders[j], true);
                }
            }
        }
    }
}
