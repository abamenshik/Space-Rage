using UnityEngine;

public class AudioSourceChecker : MonoBehaviour
{
    void Start()
    {
        var sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in sources)
        {
            //print(source.outputAudioMixerGroup);
            if (source.enabled && !source.outputAudioMixerGroup)
            {
                Debug.LogError($"�������� ����� {source.gameObject.name} �� �������� ������ �� ������!", source.gameObject);
            }
        }
    }
}