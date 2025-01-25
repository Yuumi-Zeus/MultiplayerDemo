using UnityEditor;
using YOGA.OdinToolkits;

namespace YOGA.Core.Config.Editor
{
    public static class YOGAPaths
    {
        const string YOGAName = "YOGA";

        static string _yogaRootPath;

        static YOGAPaths()
        {
            SetRootPath();
            EditorApplication.projectChanged -= SetRootPath;
            EditorApplication.projectChanged += SetRootPath;
        }

        public static string GetYOGAPath()
        {
            if (string.IsNullOrEmpty(_yogaRootPath))
            {
                SetRootPath();
            }
            return _yogaRootPath;
        }

        static void SetRootPath()
        {
            _yogaRootPath = ProjectEditorUtility.GetTargetFolderPath(YOGAName);
        }
    }
}
