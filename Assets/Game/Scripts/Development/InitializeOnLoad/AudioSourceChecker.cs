#if UNITY_EDITOR

using SpaceRage;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
static class AudioSourceChecker
{
    static AudioSourceChecker()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            ForEveryComponents.For<AudioSource>(
                (source) => source.enabled && !source.outputAudioMixerGroup,
                (source) => Debug.Log($"�������� ����� {source.gameObject.name} �� �������� ������ �� ������!", source.gameObject));
        }
    }
}
#endif