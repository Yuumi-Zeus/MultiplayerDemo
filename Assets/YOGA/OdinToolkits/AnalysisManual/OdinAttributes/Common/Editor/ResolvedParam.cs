﻿using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public enum ResolverType
    {
        ValueResolver,
        ActionResolver
    }

    public class ResolvedParam
    {
        public string ParamName;
        public ResolverType ResolverType;
        public string ReturnType;
        public List<ParamValue> ParamValues;
        GUIStyle _tableCellTextStyle;

        public GUITable CreateGUITable()
        {
            _tableCellTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineCenteredLabel)
            {
                clipping = TextClipping.Overflow,
                richText = true,
            };
            var guiTable = GUITable.Create(ParamValues, null, new GUITableColumn
                {
                    ColumnTitle = "序号",
                    Width = 50,
                    Resizable = false,
                    OnGUI = (rect, i) => { EditorGUI.LabelField(rect, (i + 1).ToString(), _tableCellTextStyle); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "返回值类型",
                    Width = 120,
                    OnGUI = (rect, i) => { DrawTableCell(rect, ParamValues[i].returnType); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "参数名",
                    Width = 120,
                    OnGUI = (rect, i) => { DrawTableCell(rect, ParamValues[i].paramName); }
                },
                new GUITableColumn
                {
                    ColumnTitle = "参数描述",
                    MinWidth = 150,
                    OnGUI = (rect, i) => { DrawTableCell(rect, ParamValues[i].paramDescription); }
                });
            return guiTable;
        }

        void DrawTableCell(Rect rect, string text, GUIStyle style = null)
        {
            EditorGUI.LabelField(rect, text, style ?? _tableCellTextStyle);
        }
    }
}
