﻿using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YOGA.Modules.OdinToolkits.Attributes;

namespace YOGA.Modules.OdinToolkits.AttributeProcessors.Editor
{
    public class StringClassAttributeProcessor : OdinAttributeProcessor<string>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            var fontsize = 12;
            var hasDisplayAsStringAlignLeftRichTextAttribute = false;
            foreach (var attribute in attributes.Where(attribute =>
                         attribute.GetType() == typeof(ForStringDisplayAsStringAlignLeftRichTextAttribute)))
            {
                hasDisplayAsStringAlignLeftRichTextAttribute = true;
                var displayAsStringAlignLeftRichTextAttribute =
                    (ForStringDisplayAsStringAlignLeftRichTextAttribute)attribute;
                fontsize = displayAsStringAlignLeftRichTextAttribute.FontSize;
                // if (property.Info.GetMemberInfo().MemberType == MemberTypes.Property)
                // {
                //     Debug.Log("正在处理 string 类型的 property 属性" + property.NiceName);
                // }
            }

            if (!hasDisplayAsStringAlignLeftRichTextAttribute) return;
            // [HideLabel, ShowInInspector, EnableGUI,
            // DisplayAsString(false, TextAlignment.Left, EnableRichText = true, FontSize = 13)]
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new ShowInInspectorAttribute());
            attributes.Add(new EnableGUIAttribute());
            attributes.Add(new DisplayAsStringAttribute(false, fontsize, TextAlignment.Left, true));
        }
    }
}