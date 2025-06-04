using System.Collections.Generic;
using Development.Attributes;
using UnityEngine;

namespace Tools.Editor
{
    [CreateAssetMenu(
        fileName = nameof(NullableFieldsOptions),
        menuName = "Editor/NullableFieldsOptions")]
    public class NullableFieldsOptions : ScriptableObject
    {
        [SerializeField, AssembleName] private List<string> checkingAssemblies;

        public List<string> CheckingAssemblies => checkingAssemblies;
    };
}