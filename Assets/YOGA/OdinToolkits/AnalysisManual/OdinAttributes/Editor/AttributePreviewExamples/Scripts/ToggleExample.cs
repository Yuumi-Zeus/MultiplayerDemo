using Sirenix.OdinInspector;
using System;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ToggleExample : ExampleScriptableObject
    {
        // 对单一字段进行修改
        [Toggle(nameof(MyToggleable.enabled))]
        public MyToggleable toggler = new MyToggleable();

        public ToggleableClass toggleable = new ToggleableClass();

        [Serializable]
        public class MyToggleable
        {
            public bool enabled;
            public int myValue;
        }

        // 可以直接把特性赋值给类，不需要对单一字段进行修改
        [Serializable][Toggle(nameof(enabled))]
        public class ToggleableClass
        {
            public bool enabled;
            public string text;
        }

    }
}