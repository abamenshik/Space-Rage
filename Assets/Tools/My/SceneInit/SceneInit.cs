#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tools.UnityEditor
{
    internal static class SceneInit
    {
        [MenuItem("GameObject/Tools/My Scene Init", false, 0)]
        private static void HierarchyOrganizeCreate(MenuCommand menuCommand)
        {
            var targetObject = Selection.activeGameObject;
            if (targetObject != null) return;

            var rootObjects = new List<GameObject>();

            var activeScene = SceneManager.GetActiveScene();
            activeScene.GetRootGameObjects(rootObjects);

            var managers = new GameObject("---  MANAGERS  ---");
            var cameras = new GameObject("---  CAMERAS  ---");
            var level = new GameObject("---  LEVEL  ---");

            var geometry = new GameObject("Geometry");
            geometry.transform.parent = level.transform;

            var decoration = new GameObject("Decoration");
            decoration.transform.parent = level.transform;

            var props = new GameObject("Props");
            props.transform.parent = level.transform;

            var enemies = new GameObject("Enemies");
            enemies.transform.parent = level.transform;

            var player = new GameObject("---  PLAYER  ---");
            var lights = new GameObject("---  LIGHTS  ---");
            var effects = new GameObject("---  EFFECTS  ---");
            var ui = new GameObject("---  UI  ---");
            CreateCanvas(ui.transform);
            var audio = new GameObject("---  AUDIO  ---");


            foreach (GameObject go in rootObjects)
            {
                if (go.TryGetComponent<Camera>(out var _))
                {
                    go.transform.parent = cameras.transform;
                    //if (camera.CompareTag("MainCamera"))
                    //{
                    //    go.transform.position = new Vector3(0, 5, -5);
                    //    go.transform.localRotation = Quaternion.Euler(45, 0, 0);
                    //    if (!camera.TryGetComponent<YuCoFlyCamera>(out var flyCamera))
                    //    {
                    //        camera.AddComponent<YuCoFlyCamera>();
                    //    }
                    //}
                }
                if (go.TryGetComponent<Light>(out var _))
                {
                    go.transform.parent = lights.transform;
                }
            }

            EditorSceneManager.MarkSceneDirty(activeScene);
        }

        private static void CreateCanvas(Transform parent)
        {
            var canvasGo = new GameObject("Canvas");
            canvasGo.transform.SetParent(parent);
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var canvasScaler = canvasGo.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasGo.AddComponent<GraphicRaycaster>();
            canvasGo.SetActive(false);

            var eventSystemGo = new GameObject("Event System");
            eventSystemGo.transform.SetParent(parent);
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();
        }
    }
}
#endif