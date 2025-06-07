using DG.Tweening;
using UnityEngine;

namespace SpaceRage
{
    public class FootVisual : MonoBehaviour
    {
        [SerializeField] private float maxZ;
        [SerializeField, Min(0)] private float duration;

        private void Awake()
        {
            var player = FindFirstObjectByType<PlayerController>();

            player.OnKickStateChange += Player_OnKickStateChange;
        }

        private void Player_OnKickStateChange(bool kickStart)
        {
            if (kickStart)
                transform.DOLocalMoveZ(maxZ, duration);
            else
                transform.DOLocalMoveZ(0, duration);
        }
    }
}
