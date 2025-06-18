using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.Components.Scenes
{
    public abstract class SceneTransitionEffectBase: MonoBehaviour
    {
        public abstract void ResetEffect();
        public abstract IEnumerator Open();
        public abstract IEnumerator Close();
    }
}