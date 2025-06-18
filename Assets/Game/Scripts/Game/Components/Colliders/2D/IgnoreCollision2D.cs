using UnityEngine;

namespace Components.Colliders
{
    public class IgnoreCollision2D : MonoBehaviour
    {
        [SerializeField] private Collider2D[] colliders;

        private void Start()
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                for (int j = 0; j < colliders.Length; j++)
                {
                    Physics2D.IgnoreCollision(colliders[i], colliders[j], true);
                }
            }
        }
    }
}
