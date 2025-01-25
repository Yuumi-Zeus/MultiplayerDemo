using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using YOGA.Modules.OdinToolkits.Editor;
using YOGA.OdinToolkits.Editor;

namespace YOGA.Modules.OdinToolkits.Scripts.Editor
{
    public class UnityEditorAPIOverviewDatabase : AbsOdinDatabase<UnityEditorAPIOverviewDatabase>
    {
        public List<string> allEditorClassAPIDataPaths = new List<string>();

        public List<string> allEditorClassAPIDataFileNames = new List<string>();

        public List<EditorClassAPIData> allEditorClassAPIData = new List<EditorClassAPIData>();

        public readonly List<Type> SirenixEditorClassRecommendedToUse = new List<Type>()
        {
            typeof(Sirenix.OdinInspector.Editor.OdinMenuEditorWindow),
            typeof(Sirenix.OdinInspector.Editor.OdinEditorWindow),
            typeof(Sirenix.OdinInspector.Editor.OdinEditor),
            typeof(Sirenix.OdinInspector.Editor.OdinMenuTree),
            typeof(Sirenix.OdinInspector.Editor.OdinMenuItem),
            typeof(Sirenix.Utilities.Editor.SirenixEditorGUI),
            typeof(Sirenix.Utilities.Editor.SirenixEditorFields),
            typeof(Sirenix.Utilities.Editor.SirenixGUIStyles),
            typeof(Sirenix.Utilities.Editor.EditorIcons),
            typeof(Sirenix.Utilities.Editor.GUIHelper),
            typeof(Sirenix.Utilities.Editor.DragAndDropUtilities),
            typeof(Sirenix.Utilities.Editor.EventExtensions),
            typeof(Sirenix.Utilities.Editor.TextureUtilities),
            typeof(Sirenix.Utilities.Editor.Clipboard),
            typeof(Sirenix.Utilities.Editor.CleanupUtility),
            typeof(Sirenix.Utilities.Editor.AssetUtilities),
            typeof(Sirenix.Utilities.Editor.GUITimeHelper)
        };

        public readonly List<Type> UnityEditorClassRecommendedToUse = new List<Type>()
        {
            typeof(GUILayout),
            typeof(GUILayoutUtility),
            typeof(EditorGUILayout),
            typeof(EditorGUI),
            typeof(EditorApplication),
            typeof(AssetDatabase),
            typeof(AssetImporter)
        };

        protected override void ClearDatabase()
        {
            allEditorClassAPIDataPaths.Clear();
            allEditorClassAPIData.Clear();
            allEditorClassAPIDataFileNames.Clear();
        }

        protected override void InitializeData()
        {
            allEditorClassAPIDataPaths = UnityEditorAPIOverviewUtil.FindAllEditorClassAPIDataPath();
            if (allEditorClassAPIDataPaths.Count > 0)
            {
                allEditorClassAPIDataFileNames =
                    allEditorClassAPIDataPaths.Select(path => path.Split('/').Last())
                        .ToList();
                allEditorClassAPIData = allEditorClassAPIDataPaths
                    .Select(AssetDatabase.LoadAssetAtPath<EditorClassAPIData>)
                    .Where(data => !data.className.IsNullOrWhitespace())
                    .ToList();
            }
        }
    }
}