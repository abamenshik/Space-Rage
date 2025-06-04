using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MetaInfoEditor : EditorWindow
{
    private TextField guidField;
    private Label guidOutput;

    private LongField instanceIdField;
    private Label instanceIdOutput;

    private LongField localIdField;
    private Label localIdOutput;


    [MenuItem("Window/My/Meta Info")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<MetaInfoEditor>();
        wnd.titleContent = new GUIContent("Meta Info");
    }

    public void CreateGUI()
    {
        guidField = new("Guid");
        guidOutput = new("None");
        var guidButton = new Button(GuidToPath)
        {
            text = "Guid"
        };

        instanceIdField = new("Instance Id");
        instanceIdOutput = new("None");
        var instanceIdButton = new Button(InstanceIdToObjectName)
        {
            text = "Instance Id"
        };

        localIdField = new("Local Id");
        localIdOutput = new("None");
        var localIdButton = new Button(LocalIdToObjectName)
        {
            text = "Local Id"
        };

        UnityEngine.UIElements.GroupBox a = new("Guid - много цифр и букв");
        a.Add(guidField);
        a.Add(guidOutput);
        a.Add(guidButton);
        rootVisualElement.Add(a);

        UnityEngine.UIElements.GroupBox b = new("Instance Id - 5 цифр");
        b.Add(instanceIdField);
        b.Add(instanceIdOutput);
        b.Add(instanceIdButton);
        rootVisualElement.Add(b);

        UnityEngine.UIElements.GroupBox c = new("Local Id - 10 цифр");
        c.Add(localIdField);
        c.Add(localIdOutput);
        c.Add(localIdButton);
        rootVisualElement.Add(c);

        //rootVisualElement.Add(new UnityEngine.UIElements.Toggle("drawGizmo"));
    }
    private void InstanceIdToObjectName()
    {
        var components = FindObjectsByType<Component>(FindObjectsSortMode.None);
        foreach (var component in components)
        {
            if (component.GetInstanceID() == instanceIdField.value)
            {
                //print($"GameObject: {component.gameObject.name}, Component: {component}");
                //print($"{component}");
                var result = component.ToString();
                instanceIdOutput.text = string.IsNullOrEmpty(result) ? "None" : result;

                return;
            }
        }
        instanceIdOutput.text = "None";
    }
    private void LocalIdToObjectName()
    {
        //var assets = AssetDatabase.FindAssets("");
        ////AssetDatabase.gui
        //if (GlobalObjectId.TryParse(assets[0], out var id))
        //{
        //    var obj = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(id);
        //    GlobalObjectId.GetGlobalObjectIdsSlow();

        //    print(obj.name);
        //}
        var components = FindObjectsByType<Component>(FindObjectsSortMode.None);
        foreach (var component in components)
        {
            PropertyInfo inspectorModeInfo =
    typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

            SerializedObject serializedObject = new(component);
            inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);

            SerializedProperty localIdProp =
                serializedObject.FindProperty("m_LocalIdentfierInFile");   //note the misspelling!

            int localId = localIdProp.intValue;
            if (localId == localIdField.value)
            //if (component.GetInstanceID() == localId)
            {
                //print($"GameObject: {component.gameObject.name}, Component: {component}");
                //print($"{component}");
                var result = component.ToString();
                localIdOutput.text = string.IsNullOrEmpty(result) ? "None" : result;

                return;
            }
        }
        localIdOutput.text = "None";
    }
    private void GuidToPath()
    {
        var path = AssetDatabase.GUIDToAssetPath(guidField.text);
        guidOutput.text = string.IsNullOrEmpty(path) ? "None" : path;
        //Debug.Log(path);
    }
    private void OnGUI()
    {
        //if (rigidbody) {
        //    rigLabel.text = rigidbody.name;
        //}
        //else {
        //    rigLabel.text = "Rigidbody not selected";
        //}
    }
    /*
    private void OnSelectionChange()
    {
        Repaint();
    }
    */
}
