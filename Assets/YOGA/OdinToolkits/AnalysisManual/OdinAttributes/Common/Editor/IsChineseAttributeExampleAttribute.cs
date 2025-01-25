using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [HelpURL(
        "https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callerfilepathattribute?view=net-9.0")]
    [AttributeUsage(AttributeTargets.Class)]
    public class IsChineseAttributeExampleAttribute : Attribute
    {
        public string FilePath { get; private set; }
        public int LineNumber { get; private set; }

        /// <summary>
        /// 标记为 Attribute Example 类
        /// </summary>
        /// <param name="filePath">可以自动获取这个特性实例所在的脚本的文件的绝对路径</param>
        /// <param name="lineNumber">可以自动获取这个特性实例所在脚本中的行数</param>
        public IsChineseAttributeExampleAttribute(
            [CallerFilePath] string filePath = "unknown",
            [CallerLineNumber] int lineNumber = -1)
        {
            FilePath = filePath;
            LineNumber = lineNumber;
        }
    }
}