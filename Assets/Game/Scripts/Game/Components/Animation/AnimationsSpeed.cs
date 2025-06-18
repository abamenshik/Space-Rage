using System;
using System.Collections.Generic;
using Extension;
using UnityEngine;

namespace Components.Animation
{
    public class AnimationsSpeed : MonoBehaviour
    {
        [Serializable]
        public class ParametersSpeed
        {
            [HideInInspector]
            [Min(0)] public string name;
            [Min(0)] public float speed;

            public ParametersSpeed(string name, float speed)
            {
                this.name = name;
                this.speed = speed;
            }
        }
        [Header("ѕараметры аниматора, отвечающие за скорость анимаций")]
        [Header("должны иметь постфикс animSpeed (регистр не важен)")]
        [Space]

        [SerializeField] private List<ParametersSpeed> speeds = new();

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();

            foreach (var parameter in speeds)
            {
                _animator.SetFloat(parameter.name, parameter.speed);
            }
        }

        private void OnValidate()
        {
            _animator = GetComponentInChildren<Animator>();

            if (speeds.IsInited())
            {
                foreach (var parameter in speeds)
                {
                    _animator.SetFloat(parameter.name, parameter.speed);
                }
            }
            else
            {
                foreach (var parameter in _animator.parameters)
                {
                    if (parameter.type == AnimatorControllerParameterType.Float &&
                        parameter.name.Contains("animSpeed", StringComparison.InvariantCultureIgnoreCase))
                    {
                        speeds.Add(new(parameter.name, parameter.defaultFloat));
                    }
                }
            }
        }
    }
}
