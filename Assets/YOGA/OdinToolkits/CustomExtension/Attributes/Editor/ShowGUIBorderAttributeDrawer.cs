using Sirenix.OdinInspector.Editor;
using UnityEngine;
using YOGA.OdinToolkits;

namespace YOGA.Modules.OdinToolkits.Attributes.Editor
{
    [DrawerPriority(0, 10)]
    public class ShowGUIBorderAttributeDrawer : OdinAttributeDrawer<ShowGUIBorderAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
            YogaEditorGUIUtility.DrawRectOutlineWithBorder(GUILayoutUtility.GetLastRect(), Color.green);
        }
    }
}