using Scripts.Extension;
using UnityEngine;

namespace Components.Triggers
{
    public abstract class TriggerBase : MonoBehaviour, ITriggerable
    {
        public bool ShowVisual { get; set; } = true;

        [SerializeField] private GameObject[] triggered;

        private bool wasTriggerd;


        public void Trigger()
        {
            if (wasTriggerd) return;

            wasTriggerd = true;

            foreach (var obj in triggered)
            {
                if (obj.TryGetComponent(out ITriggerable trigered))
                {
                    trigered.Trigger();

                    Debug.Log($"{obj.name.Color(TextColor.red)} был стриггерен объектом {gameObject.name.Color(TextColor.cyan)}", this);
                }
                else
                {
                    Debug.LogError($"{obj.name.Color(TextColor.red)} не имеет реакции на триггер", this);
                }
            }
        }
        protected abstract void ForbitChangingColliderSizeAndCenter();

#if !UNITY_EDITOR
    private void Awake()
    {
        DestroyVisual(); 
    }

    private void DestroyVisual()
    {
        if (TryGetComponent(out MeshRenderer renderer))
        {
            Destroy(renderer);
        }
        if (TryGetComponent(out MeshFilter filter))
        {
            Destroy(filter);
        }
    }
#endif
#if UNITY_EDITOR
        protected virtual void DrawGizmos() { }
        private void OnDrawGizmos()
        {
            // вызывается в OnDrawGizmos, т.к. OnValidate не вызывается при изменении размера коллайдера
            // а нужно чтобы сразу значение не обновлялось
            ForbitChangingColliderSizeAndCenter();

            if (!ShowVisual)
                return;

            DrawGizmos();

            if (wasTriggerd || triggered == null || triggered.Length == 0)
            {
                return;
            }

            Gizmos.color = Color.red;

            foreach (var obj in triggered)
            {
                if (!obj)
                    continue;

                Gizmos.DrawLine(transform.position, obj.transform.position);
            }
        }
#endif
    }
}