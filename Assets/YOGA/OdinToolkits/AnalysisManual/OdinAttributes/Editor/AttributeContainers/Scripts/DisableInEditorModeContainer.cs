using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DisableInEditorModeContainer : AbsContainer
    {
        protected override string SetHeader() => "DisableInEditorMode";

        protected override string SetBrief() => "让 Property 无法在编辑模式下选中";

        protected override List<string> SetTip() => new List<string>()
        {
            "适合运行时需要调试的数据"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
            { };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInEditorModeExample));
    }
}