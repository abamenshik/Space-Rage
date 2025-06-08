using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceRage
{
    public class ImpulseBar : MonoBehaviour
    {
        [SerializeField] private Image bar;

        private PlayerController player;

        void Start()
        {
            player = FindFirstObjectByType<PlayerController>();
            player.impulseCd.OnReset += ImpulseCd_OnReset;

            bar.fillAmount = 0;
        }
        private void OnDestroy()
        {
            player.impulseCd.OnReset -= ImpulseCd_OnReset;
        }
        private void ImpulseCd_OnReset()
        {
            bar.fillAmount = 1;

            bar.DOFillAmount(0, player.impulseCd.Value);
        }
    }
}
