using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Editor.SyntaxHighlighter
{
    /// <summary>
    /// 获取 Odin 内部的语法高亮处理器并封装，通过此 Presenter 可以借助 Odin 的工具实现语法高亮
    /// </summary>
    public class OdinSyntaxHighlighterPresenter : SerializedScriptableObject
    {
        [PropertyOrder(-10)]
        [Title("使用须知", "Odin 的语法高亮处理有一定局限性", TitleAlignments.Centered)]
        [OnInspectorGUI]
        void OnGUI1() { }

        [DisplayAsString(TextAlignment.Left, FontSize = 14)]
        [HideLabel]
        public string one = "1. 需要处理的源代码中，不能包含有命名空间";

        [DisplayAsString(TextAlignment.Left, FontSize = 14)]
        [HideLabel]
        public string two = "2. 需要处理的源代码中，不能包含有 $ 内插字符串";

        [Title("Odin 语法高亮的默认颜色配置")]
        [ReadOnly]
        [ShowInInspector]
        public static Color BackgroundColor = new Color(0.118f, 0.118f, 0.118f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color TextColor = new Color(0.863f, 0.863f, 0.863f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color KeywordColor = new Color(0.337f, 0.612f, 0.839f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color IdentifierColor = new Color(0.306f, 0.788f, 0.69f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color CommentColor = new Color(0.341f, 0.651f, 0.29f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color LiteralColor = new Color(0.71f, 0.808f, 0.659f, 1f);

        [ReadOnly]
        [ShowInInspector]
        public static Color StringLiteralColor = new Color(0.839f, 0.616f, 0.522f, 1f);

        public List<CustomSyntaxHighlighterColorGroup>
            customColorGroups = new List<CustomSyntaxHighlighterColorGroup>();

        public static Type 语法高亮处理器类型 = Type.GetType(
            "Sirenix.OdinInspector.Editor.Examples.SyntaxHighlighter," +
            "Sirenix.OdinInspector.Editor," +
            "Version=1.0.0.0," +
            "Culture=neutral," +
            "PublicKeyToken=null");

        public static readonly MethodInfo ParseMethod =
            语法高亮处理器类型.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);

        public static string ApplyCodeHighlighting(string code)
        {
            if (ParseMethod == null)
            {
                Debug.LogError("无法获取 SyntaxHighlighter.Parse 方法");
                return string.Empty;
            }

            return ParseMethod.Invoke(null, new object[] { code }) as string;
        }

        [Button(ButtonSizes.Gigantic)]
        public void 测试语法高亮()
        {
            const string code = @"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : ScriptableObject
{
    public int ExampleInt;
    public float ExampleFloat;
    public string ExampleString;
    public bool ExampleBool;
    public Vector3 ExampleVector3;
    public Color ExampleColor;
    public GameObject ExampleGameObject;
    public List<int> ExampleList;
    public Dictionary<string, int> ExampleDictionary;       
}";
            Debug.Log(ApplyCodeHighlighting(code));
        }
    }

    [Serializable]
    public class CustomSyntaxHighlighterColorGroup
    {
        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("背景颜色")]
        [InfoBox("绘制背景时可以设置为此颜色，此颜色默认不参与代码颜色设置")]
        public Color backgroundColor;

        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("普通文本颜色")]
        public Color textColor;

        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("关键字颜色")]
        public Color keywordColor;

        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("标识符颜色")]
        public Color identifierColor;

        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("注释颜色")]
        public Color commentColor;

        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("字面量颜色")]
        public Color literalColor;

        [BoxGroup(ShowLabel = false)]
        [ColorPalette]
        [LabelText("字符串颜色")]
        public Color stringLiteralColor;
    }
}