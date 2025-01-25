using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace YOGA.Modules.OdinToolkits.NorthWindStudios
{
    internal sealed class ScriptableObjectMakerSettingsProvider : SettingsProvider
    {
        public ScriptableObjectMakerSettingsProvider(string path,
                                                     SettingsScope scopes,
                                                     IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var scriptableObjectMakerSettingsSerializedObject =
                new SerializedObject(ScriptableObjectMakerSettings.instance);

            SerializedObject serializedObject = scriptableObjectMakerSettingsSerializedObject;

            SerializedProperty ignoreAssemblyNamesProperty = serializedObject.FindProperty(
                $"<{nameof(ScriptableObjectMakerSettings.instance.IgnoreAssemblyNames)}>k__BackingField");
            SerializedProperty sortAssembliesProperty = serializedObject.FindProperty(
                $"<{nameof(ScriptableObjectMakerSettings.instance.SortPriorityAssemblies)}>k__BackingField");


            var header = new Label("Scriptable Object Maker")
            {
                style =
                {
                    fontSize = 20,
                    unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold),
                    paddingLeft = 10,
                    paddingTop = 2,
                    paddingRight = 2,
                    paddingBottom = 2
                }
            };
            rootElement.Add(header);

            var companyLabel = new Label("From " + StringSource.Company)
            {
                style =
                {
                    paddingLeft = 5,
                    paddingTop = 3,
                    paddingRight = 3,
                    paddingBottom = 3
                }
            };
            companyLabel.SetEnabled(false);
            rootElement.Add(companyLabel);

            var versionLabel = new Label("Version: " + StringSource.Version)
            {
                style =
                {
                    paddingLeft = 5,
                    paddingTop = 3,
                    paddingRight = 3,
                    paddingBottom = 3
                }
            };
            versionLabel.SetEnabled(false);
            rootElement.Add(versionLabel);

            rootElement.Add(new VisualElement { style = { height = 10 } });

            var p1 = new PropertyField(ignoreAssemblyNamesProperty);
            p1.Bind(scriptableObjectMakerSettingsSerializedObject);
            rootElement.Add(p1);

            var p2 = new PropertyField(sortAssembliesProperty);
            p2.Bind(scriptableObjectMakerSettingsSerializedObject);
            rootElement.Add(p2);

            rootElement.Add(new VisualElement { style = { height = 10 } });

            rootElement.Add(
                new Button(
                    () => { Application.OpenURL(StringSource.PackageAssetStorePageLink); })
                {
                    text = "Open Store Page",
                    style = { height = 24 }
                });

            rootElement.Add(new Button(ScriptableObjectMakerSettings.ResetToDefault)
            {
                text = "Reset to Default",
                style = { height = 24 }
            });
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new ScriptableObjectMakerSettingsProvider(StringSource.PreferencesPath, SettingsScope.User);
        }
    }
}
