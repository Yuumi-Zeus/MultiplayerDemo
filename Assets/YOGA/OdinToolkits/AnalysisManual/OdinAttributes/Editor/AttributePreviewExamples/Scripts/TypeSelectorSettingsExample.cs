using Sirenix.OdinInspector;
using System;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class TypeSelectorSettingsExample : ExampleScriptableObject
    {
        [FoldoutGroup("Odin 默认绘制的 Type 选择器")]
        [ShowInInspector]
        public Type Default;

        [FoldoutGroup("PreferNamespaces 参数")]
        [ShowInInspector]
        [LabelText("On")]
        [TypeSelectorSettings(PreferNamespaces = true, ShowCategories = false, ShowNoneItem = false)]
        public Type PreferNamespaces_On;

        [FoldoutGroup("PreferNamespaces 参数")]
        [ShowInInspector]
        [LabelText("Off")]
        [TypeSelectorSettings(PreferNamespaces = false, ShowCategories = false, ShowNoneItem = false)]
        public Type PreferNamespaces_Off;

        [FoldoutGroup("ShowCategories 参数")]
        [ShowInInspector]
        [LabelText("On")]
        [TypeSelectorSettings(ShowCategories = true, PreferNamespaces = false, ShowNoneItem = false)]
        public Type ShowCategories_On;

        [FoldoutGroup("ShowCategories 参数")]
        [ShowInInspector]
        [LabelText("Off")]
        [TypeSelectorSettings(ShowCategories = false, PreferNamespaces = false, ShowNoneItem = false)]
        public Type ShowCategories_Off;

        [FoldoutGroup("ShowNoneItem 参数")]
        [ShowInInspector]
        [LabelText("On")]
        [TypeSelectorSettings(ShowNoneItem = true, PreferNamespaces = false, ShowCategories = false)]
        public Type ShowNoneItem_On;

        [FoldoutGroup("ShowNoneItem 参数")]
        [ShowInInspector]
        [LabelText("Off")]
        [TypeSelectorSettings(ShowNoneItem = false, PreferNamespaces = false, ShowCategories = false)]
        public Type ShowNoneItem_Off;

        [FoldoutGroup("FilterTypesFunction 参数")]
        [ShowInInspector]
        [TypeSelectorSettings(FilterTypesFunction = nameof(TypeFilter), ShowCategories = true)]
        public Type CustomTypeFilterExample;

        bool TypeFilter(Type type) =>
            type.IsAbstract == false && typeof(ExampleScriptableObject).IsAssignableFrom(type);
    }
}