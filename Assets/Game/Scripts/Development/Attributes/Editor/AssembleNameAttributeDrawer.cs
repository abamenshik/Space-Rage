using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Development.Attributes
{
    [CustomPropertyDrawer(typeof(AssembleNameAttribute))]
    public class AssembleNameAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where((a, b) =>
            {
                return !a.FullName.Contains("UnityEngine")
                && !a.FullName.Contains("UnityEditor")
                && !a.FullName.Contains("Unity")
                && !a.FullName.Contains("System");
            });
            List<string> a = new();
            foreach (var item in allAssemblies)
            {
                a.Add(item.FullName);
            }
            //получить индекс в списке строк, соответствующий строке, выбранной в выпадающем списке
            int index = Mathf.Max(a.IndexOf(property.stringValue), 0);

            index = EditorGUI.Popup(position, property.displayName, index, a.ToArray());
            //обновить надпись выбранного элемента 
            property.stringValue = a[index];
        }
    }
}