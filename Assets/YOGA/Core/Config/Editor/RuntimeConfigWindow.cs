using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace YOGA.Core.Config.Editor
{
    public class RuntimeConfigWindow : OdinEditorWindow
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public YogaRuntimeConfigSO runtimeConfigSO;

        [MenuItem("YOGA/运行时配置面板", false, 0)]
        static void OpenWindow()
        {
            var window = GetWindow<RuntimeConfigWindow>();
            window.titleContent = new GUIContent("Yoga 框架运行时配置窗口");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 400);
            window.Show();
            window.runtimeConfigSO = YogaRuntimeConfigSO.Instance;
        }
    }
}
