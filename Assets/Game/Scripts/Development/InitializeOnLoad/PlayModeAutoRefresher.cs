
// �� �������� ���� ������ � ����� Editor
// �� ��������� ��������

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public static class PlayModeAutoRefresher
{
    static PlayModeAutoRefresher()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            AssetDatabase.Refresh();
        }
    }
} 
#endif