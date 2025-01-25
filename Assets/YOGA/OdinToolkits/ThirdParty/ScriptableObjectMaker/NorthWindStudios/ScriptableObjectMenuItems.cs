using System;
using UnityEditor;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.NorthWindStudios
{
    internal class ScriptableObjectMenuItems
    {
        private static AdvancedScriptableObjectMakerDropdown advancedScriptableObjectMaker;

        [MenuItem("Assets/Create Scriptable Object from Selected", true)]
        private static bool CanCreateScriptableObjectFromSelected()
        {
            return Selection.activeObject is MonoScript script &&
                   !script.GetClass().IsAbstract &&
                   script.GetClass().IsSubclassOf(typeof(ScriptableObject));
        }

        [MenuItem("Assets/Create Scriptable Object from Selected")]
        private static void CreateScriptableObjectFromSelected()
        {
            if (Selection.activeObject is MonoScript script)
            {
                ProjectWindowUtil.CreateAsset(
                    ScriptableObject.CreateInstance(script.GetClass()),
                    $"{script.name}.asset");
            }
        }

        [MenuItem("Assets/Create Scriptable Object")]
        private static void CreateScriptableObject()
        {
            advancedScriptableObjectMaker =
                new AdvancedScriptableObjectMakerDropdown(CreateScriptableObject);

            EditorApplication.projectWindowItemOnGUI += ShowAdvancedDropDown;
        }

        private static void CreateScriptableObject(Type type)
        {
            ProjectWindowUtil.CreateAsset(
                ScriptableObject.CreateInstance(type),
                $"{type.Name}.asset");
        }

        private static void ShowAdvancedDropDown(string guid, Rect selectionRect)
        {
            advancedScriptableObjectMaker?.
                Show(new Rect(Event.current.mousePosition, Vector2.zero));
            EditorApplication.projectWindowItemOnGUI -= ShowAdvancedDropDown;
        }
    }
}
