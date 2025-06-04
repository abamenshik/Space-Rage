using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PathResourcesAttribute))]
public class PathResourcesAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var paths = PathResources.ALL_PATHS;

        //�������� ������ � ������ �����, ��������������� ������, ��������� � ���������� ������
        int index = Mathf.Max(paths.IndexOf(property.stringValue), 0);

        index = EditorGUI.Popup(position, property.displayName, index, paths.ToArray());

        //�������� ������� ���������� �������� 
        property.stringValue = paths[index];
    }
}
