using Sirenix.OdinInspector;
using YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Scripts;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ShowInInlineEditorExample : ExampleScriptableObject
    {
        [InlineEditor]
        public CommonInlineObject commonInlineObject;
    }
}