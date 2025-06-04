namespace SpaceRage
{
    using Scripts.Extension;
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine;

    [InitializeOnLoad]
    public static class IsActivity
    {
        static IsActivity()
        {
            //EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                ForEveryComponents.For<Behaviour>(
                    (t) => t.enabled,
                    (t) => Debug.Log(t.GetType().Name + " is enabled".Color(Random.ColorHSV())));
            }
        }
    }
#endif
}