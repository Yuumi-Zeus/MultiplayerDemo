using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.NorthWindStudios
{
    internal class AdvancedScriptableObjectMakerDropdown : AdvancedDropdown
    {
        private Action<Type> onTypeSelected;

        public AdvancedScriptableObjectMakerDropdown(Action<Type> onTypeSelected) : base(new AdvancedDropdownState())
        {
            this.onTypeSelected = onTypeSelected;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            if (item is AdvancedRateUsDropdownItem rateUsItem)
                rateUsItem.OpenAssetStorePage();
            else if (item is AdvancedTypeDropdownItem selectedItem)
                onTypeSelected(selectedItem.type);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            Type baseType = typeof(ScriptableObject);

            string[] ignoreAssemblyNames =
                ScriptableObjectMakerSettings.instance.IgnoreAssemblyNames;

            List<string> sortPriorityAssemblies =
                ScriptableObjectMakerSettings.instance.SortPriorityAssemblies;

            var ignoreBaseTypes = new List<Type>
            {
                typeof(EditorWindow),
                typeof(Editor)
            };

            var root = new AdvancedDropdownItem("Subclasses of Scriptable Object");

            if (ScriptableObjectMakerSettings.instance.ShowRateUsOption)
                root.AddChild(new AdvancedRateUsDropdownItem());

            foreach (string priorityAssembly in sortPriorityAssemblies)
            {
                if (!string.IsNullOrWhiteSpace(priorityAssembly) && !ignoreAssemblyNames.Contains(priorityAssembly))
                    root.AddChild(new AdvancedDropdownItem(priorityAssembly));
            }

            root.AddSeparator();

            var childTypes = TypeCache.GetTypesDerivedFrom(baseType).ToList();

            childTypes.RemoveAll(
                _ =>
                    _.IsAbstract ||
                    ignoreAssemblyNames.Any(i => _.Assembly.GetName().Name.StartsWith(i)) ||
                    ignoreBaseTypes.Any(_.IsSubclassOf));

            childTypes.Sort(
                (t1, t2) =>
                {
                    string t1Assembly = t1.Assembly.GetName().Name;
                    string t2Assembly = t2.Assembly.GetName().Name;

                    int t1Priority = sortPriorityAssemblies.FindIndex(_ => t1Assembly.Contains(_));
                    int t2Priority = sortPriorityAssemblies.FindIndex(_ => t2Assembly.Contains(_));

                    if (t1Priority != t2Priority)
                        return t2Priority - t1Priority;

                    int comparison =
                        string.Compare(t1Assembly, t2Assembly, StringComparison.Ordinal);

                    return comparison != 0 ?
                               comparison :
                               string.Compare(t1.FullName, t2.FullName, StringComparison.Ordinal);
                });

            foreach (Type childType in childTypes)
            {
                if (childType == null)
                    continue;

                string[] parents = childType.Assembly.GetName().Name.Split(".");
                if (childType.Namespace != null)
                    parents = parents.Union(childType.Namespace.Split(".")).ToArray();

                AdvancedDropdownItem parent = FindRecursiveFinalParentItem(root, parents);
                parent.AddChild(new AdvancedTypeDropdownItem(childType));
            }

            return root;
        }

        private static AdvancedDropdownItem FindRecursiveFinalParentItem(AdvancedDropdownItem current,
                                                                         string[] parentNames,
                                                                         int index = 0)
        {
            for (; index < parentNames.Length; index++)
            {
                foreach (AdvancedDropdownItem child in current.children)
                {
                    if (child.name == parentNames[index])
                        return FindRecursiveFinalParentItem(child, parentNames, index + 1);
                }

                var newChild = new AdvancedDropdownItem(parentNames[index]);
                current.AddChild(newChild);

                current = newChild;
            }

            return current;
        }
    }

    internal class AdvancedTypeDropdownItem : AdvancedDropdownItem
    {
        public Type type;

        public AdvancedTypeDropdownItem(Type type) : base(type != null ? type.Name : "None")
        {
            this.type = type;
        }
    }

    internal class AdvancedRateUsDropdownItem : AdvancedDropdownItem
    {
        public AdvancedRateUsDropdownItem() : base("Please Review Our Package, If You Liked It.")
        {
            icon = (Texture2D)EditorGUIUtility.IconContent("alertDialog").image;
        }

        public void OpenAssetStorePage()
        {
            Application.OpenURL(StringSource.PackageAssetStorePageLink);
            ScriptableObjectMakerSettings.instance.ShowRateUsOption = false;
        }
    }
}
