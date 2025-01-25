using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace YOGA.Core.Config.Editor
{
    public class EditorSettingsWindow : OdinEditorWindow
    {
        [MenuItem("YOGA/编辑器设置面板", false, 15)]
        static void OpenWindow()
        {
            var window = GetWindow<EditorSettingsWindow>();
            window.titleContent = new GUIContent("Yoga 框架编辑器设置窗口");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 400);
            window.Show();
        }

        [Button("删除冗余的 Packages", ButtonSizes.Large)]
        public void RemoveRedundantPackages()
        {
            if (EditorUtility.DisplayDialog("删除冗余的 Packages",
                    "此操作将会删除 VisualScripting 和 VersionControls Packages", "确认", "取消"))
            {
                var packagesToRemove = new List<string>
                {
                    "com.unity.visualscripting",
                    "com.unity.collab-proxy"
                };
                foreach (var packageName in packagesToRemove)
                {
                    Client.Remove(packageName);
                    Debug.Log($"Removed Package: {packageName}");
                }
            }
        }
    }
}
