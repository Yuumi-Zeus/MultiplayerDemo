using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using YOGA.Modules.OdinToolkits.Editor.GenerateTemplateCode.Scripts;
using YOGA.Modules.OdinToolkits.Editor.GenerateTemplateCode.Specific.AttributeAnalysisGenCode;
using YOGA.OdinToolkits;
using YOGA.OdinToolkits.Config.Editor;

namespace YOGA.Modules.OdinToolkits.Editor.Window
{
    public class ToolsPackageWindow : OdinMenuEditorWindow
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(OdinToolkitsMenuPaths.ToolsPackagePath, false, OdinToolkitsMenuPaths.ToolsPackagePriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<ToolsPackageWindow>();
            window.titleContent = new GUIContent("Odin Toolkits 工具合集包");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.AddObjectAtPath("通用工具/模板代码生成工具",
                ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<GenTemplateCodeTool>());
            tree.AddObjectAtPath("特殊定制/特性解析代码生成工具",
                ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<AttributeAnalysisGenCodeTool>());
            return tree;
        }
    }
}