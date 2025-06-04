using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Scripts.Extension;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Tools.Editor
{
    // TODO: может добавить проверку коллекций ссылок
    public class ShowNullableFields : EditorWindow
    {
        private List<string> checkingAssemblies;
        private BindingFlags bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
        private ScrollView parent;
        private List<object> fieldPath = new();


        [DidReloadScripts]
        [MenuItem("Window/My/NullableFields")]
        private static void ShowEditor()
        {
            var window = GetWindow<ShowNullableFields>();
            window.titleContent = new GUIContent("Nullable Fields");
        }

        private void AddNullReferenceVisual(string fieldName, MonoBehaviour component)
        {
            var itemElement = new VisualElement();
            itemElement.style.flexDirection = FlexDirection.Row;
            itemElement.focusable = true;

            fieldPath.Remove(fieldName);

            var name = GetPath();

            var label = new Label($"В компоненте {name.Color(TextColor.green)} не прокинута ссылка в поле {fieldName.Color(TextColor.cyan)}");
            label.style.flexGrow = 1f;


            var button = new Button();
            button.text = "Select";
            button.clicked += () => Selection.activeObject = component;

            itemElement.Add(label);
            itemElement.Add(button);

            parent.Add(itemElement);
        }
        private void OnEnable()
        {
            rootVisualElement.Clear();

            parent = new ScrollView();


            Do();


            var button = new Button();
            button.text = "Refresh";
            button.clicked += OnEnable;

            parent.Add(button);
            rootVisualElement.Add(parent);
        }
        private void Do()
        {
            var monoBehaviours = Object.FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include, FindObjectsSortMode.None);

            var option = Resources.Load<NullableFieldsOptions>("NullableFieldsOptions");
            if (option == null)
            {
                Debug.LogError("В папку ресурсов не добавлен файл NullableFieldsOptions для настроек окна с нулевыми ссылками!");
                return;
            }
            checkingAssemblies = option.CheckingAssemblies;
            if (checkingAssemblies == null || checkingAssemblies.Count == 0)
            {
                Debug.LogError("Файл настроек для окна с нулевыми ссылками не инициализирован!");
                return;
            }

            foreach (var beh in monoBehaviours)
            {
                var type = beh.GetType();
                if (checkingAssemblies.Contains(type.Assembly.FullName))
                {
                    // BindingFlags.FlattenHierarchy вернет public и protected поля из родительского класса
                    // 2я строка поля ссылки на монобехи, геймобджекты и тп а также кастомные классы которые я написал  
                    // 3я поля видимые в инспекторе
                    var fields = type.GetFields(bindings).
                Where(field => (field.FieldType.IsSubclassOf(typeof(UnityEngine.Object)) || checkingAssemblies.Contains(field.FieldType.Assembly.FullName))
                && (field.IsPublic || field.IsDefined(typeof(SerializeField)))).ToArray();

                    var behName = type.Name;

                    fieldPath.Add(behName);

                    // обязательно так пробегаться, так как я не знаю как отличить
                    // экземпляр типа (т.е. монобех на сцене) и поле ссылку на этот тип
                    foreach (var field in fields)
                    {
                        if (!RecursivelyFields(field, beh, beh))
                        {
                            continue;
                        }
                    }
                    fieldPath.Remove(behName);
                }
            }
        }
        private bool RecursivelyFields(FieldInfo field, object context, MonoBehaviour component)
        {
            fieldPath.Add(R(field.Name));
            // если си шарп класс, проверить его на ссылки
            // если это ссылка на компонент, не проверять
            if (checkingAssemblies.Contains(field.FieldType.Assembly.FullName) && !field.FieldType.IsSubclassOf(typeof(UnityEngine.Object)))
            {
                var fields = field.FieldType.GetFields(bindings).
                   Where(field => (field.FieldType.IsSubclassOf(typeof(UnityEngine.Object)) || checkingAssemblies.Contains(field.FieldType.Assembly.FullName))
                   && (field.IsPublic || field.IsDefined(typeof(SerializeField)))).ToArray();

                foreach (var f in fields)
                {
                    RecursivelyFields(f, field.GetValue(context), component);
                }
            }

            if (context == null)
            {
                fieldPath.Remove(R(field.Name));

                return false;
            }

            var value = field.GetValue(context);

            try
            {
                // только такое сравнение на null работает
                if (value.ToString() != "null")
                {
                    fieldPath.Remove(R(field.Name));

                    return false;
                }
            }
            // если поле только что было добавлено, то оно равно не "null" а null
            // и ToString выбросит ошибку, при этом если проверять value != null
            // то вся логика ломается, поэтому ловлю ошибку, т.е. если появилась ошибка,
            // то поле не имеет ссылки, это наш клиент
            catch (Exception)
            {
            }
            //Debug.Log($"В компоненте {context} не прокинута ссылка в поле {field.Name}");

            var fieldName = R(field.Name);

            AddNullReferenceVisual(fieldName, component);

            return true;
        }
        private string R(string name)
        {
            // у сериализованных свойств имя имеет вид <MyProperty>k__BackingField, я беру что внутри скобок

            var indexEnd = name.IndexOf('>');

            return indexEnd < 0 ? name : name[1..indexEnd];
        }
        private string GetPath()
        {
            var result = string.Empty;
            for (int i = 0; i < fieldPath.Count; i++)
            {
                object item = fieldPath[i];
                result += item.ToString();

                if (i < fieldPath.Count - 1)
                    result += " -> ";
            }
            return result;
        }
    }
}