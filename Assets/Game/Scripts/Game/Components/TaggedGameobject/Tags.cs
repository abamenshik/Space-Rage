using Scripts.Gameplay.Tags;
using UnityEngine;

public static class Tags
{
    public static bool TryGetTagged<T>(out T tag) where T : TaggedGameobject
    {
        tag = Object.FindFirstObjectByType<T>();

        if (!tag)
            Debug.LogError($"В сцене нет объекта помеченного тегом {typeof(T).Name}");

        return tag;
    }
}