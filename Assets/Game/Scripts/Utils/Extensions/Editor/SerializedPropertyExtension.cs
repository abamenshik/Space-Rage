using System;
using UnityEditor;

namespace Scripts.Utils.Extension.Editor
{
    public static class SerializedPropertyExtension {
        public static bool TryGetEnum<TEnumType>(this SerializedProperty property, out TEnumType result)
            where TEnumType : Enum {
            result = default;
            string[] names = property.enumNames;
            if (names == null || names.Length == 0)
                return false;
            string enumName = names[property.enumValueIndex];
            result = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);
            return true;
        }
    }
}