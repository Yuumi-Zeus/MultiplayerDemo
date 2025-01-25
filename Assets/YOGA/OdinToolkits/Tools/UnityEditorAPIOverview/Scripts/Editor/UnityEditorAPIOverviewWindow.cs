using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using YOGA.Modules.OdinToolkits.Editor;
using YOGA.Modules.OdinToolkits.Attributes;
using YOGA.OdinToolkits.Config.Editor;

namespace YOGA.Modules.OdinToolkits.Scripts.Editor
{
    public class UnityEditorAPIOverviewWindow : OdinMenuEditorWindow
    {
        [ForStringDisplayAsStringAlignLeftRichText(16)]
        public string unityVersionMajor = "当前 Unity 版本为: " + UnityVersion.Major;

        static UnityEditorAPIOverviewDatabase OverviewDatabase => UnityEditorAPIOverviewUtil.OverviewDatabase;

        protected override void OnEnable()
        {
            base.OnEnable();
            MenuWidth = 220;
            ResizableMenuWidth = true;
            WindowPadding = new Vector4(10, 10, 10, 10);
            DrawUnityEditorPreview = true;
            DefaultEditorPreviewHeight = 20;
            UseScrollView = true;
        }

        [Button("生成 Data 文件", ButtonSizes.Large)]
        void FindAllEditorClassAPIData()
        {
            foreach (var type in OverviewDatabase.SirenixEditorClassRecommendedToUse)
                UnityEditorAPIOverviewUtil.CreateEditorClassAPIData(type,
                    OdinToolkitsPaths.GetRootPath() +
                    "/Development Tools/1_Unity Editor API Overview/ClassAPIDataAssets");

            foreach (var type in OverviewDatabase.UnityEditorClassRecommendedToUse)
                UnityEditorAPIOverviewUtil.CreateEditorClassAPIData(type,
                    OdinToolkitsPaths.GetRootPath() +
                    "/Development Tools/1_Unity Editor API Overview/ClassAPIDataAssets");
        }

        [MenuItem(OdinToolkitsMenuPaths.OdinToolsMenuItemPath + "/" + "Unity Editor API Overview", false, 80)]
        static void OpenWindow()
        {
            var win = GetWindow<UnityEditorAPIOverviewWindow>();
            win.titleContent = new GUIContent("Unity 编辑器相关 API 查询器",
                EditorIcons.OdinInspectorLogo);
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 600);
            win.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false)
            {
                {
                    "主界面", this
                },
                {
                    "Sirenix", null, EditorIcons.OdinInspectorLogo
                },
                {
                    "Unity Build In", null, EditorGUIUtility.IconContent("UnityLogo").image
                }
            };

            foreach (var apiData in OverviewDatabase.allEditorClassAPIData)
            {
                var path = apiData.belongToCollection + "/" + apiData.className;
                switch (apiData.className)
                {
                    case "SirenixEditorGUI":
                    case "SirenixEditorFields":
                    case "SirenixGUIStyles":
                    case "EditorIcons":
                    case "GUILayout":
                    case "GUILayoutUtility":
                    case "GUI":
                    case "EditorGUILayout":
                    case "EditorGUI":
                    case "EditorApplication":
                    case "EditorUtility":
                        tree.Add(path, apiData, SdfIconType.LightbulbFill);
                        break;
                    default:
                        tree.Add(path, apiData);
                        break;
                }
            }

            tree.Config.DrawSearchToolbar = true;
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}
