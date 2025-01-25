using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.NorthWindStudios
{
    [FilePath("ScriptableObjectMaker/ScriptableObjectMakerSettings.asset",
              FilePathAttribute.Location.PreferencesFolder)]
    internal class ScriptableObjectMakerSettings : ScriptableSingleton<ScriptableObjectMakerSettings>
    {
        [SerializeField]
        [HideInInspector]
        private bool showRateUsOption = true;

        [field: SerializeField]
        [field: Tooltip("Any scriptable object within the assembly names in the list " +
                        "will not show up in the advanced dropdown menu " +
                        "when using the \"Create Scriptable Object\" menu option.")]
        public string[] IgnoreAssemblyNames { get; set; }

        [field: SerializeField]
        [field: Tooltip("In the advanced dropdown menu " +
                        "when using the \"Create Scriptable Object\" menu option, " +
                        "everything is sorted by names. " +
                        "This option separates mentioned assembly names in the list " +
                        "and sort them based on the list instead.")]
        public List<string> SortPriorityAssemblies { get; set; }

        public bool ShowRateUsOption
        {
            get => showRateUsOption;
            set
            {
                showRateUsOption = value;
                Save();
            }
        }

        private void OnEnable()
        {
            IgnoreAssemblyNames ??= GetDefaultIgnoreAssemblyNames();
            SortPriorityAssemblies ??= GetDefaultInstanceSortPriorityAssemblies();

            hideFlags = ~HideFlags.NotEditable;
        }

        public static void Save()
        {
            instance.Save(true);
        }

        public static void ResetToDefault()
        {
            instance.IgnoreAssemblyNames = GetDefaultIgnoreAssemblyNames();
            instance.SortPriorityAssemblies = GetDefaultInstanceSortPriorityAssemblies();

            Save();
        }

        private static string[] GetDefaultIgnoreAssemblyNames()
        {
            return new[]
            {
                "ScriptableObjectMaker",
                "Google",
                "JetBrains",
                "RainbowFolders",
                "RainbowCore"
            };
        }

        private static List<string> GetDefaultInstanceSortPriorityAssemblies()
        {
            return new List<string>
            {
                "Assembly-CSharp",
                "Assembly-CSharp-firstpass",
                "Assembly-CSharp-Editor",
                "Assembly-CSharp-Editor-firstpass",
                "UnityEngine",
                "UnityEditor"
            };
        }
    }
}
