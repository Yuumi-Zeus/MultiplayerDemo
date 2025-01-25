using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using YOGA.Core.Architecture;
using YOGA.Core.Config.Editor;
using YOGA.OdinToolkits;

namespace YOGA.Core.Config
{
    public class YogaRuntimeConfigSO :
#if UNITY_EDITOR
        SerializedScriptableObject
#else
        ScriptableObject
#endif
    {
        static string YogaRuntimeConfigSOPath
        {
            get => YOGAPaths.GetYOGAPath() + "/Core/Config/Resources/YogaRuntimeConfigSO.asset";
        }

        public static YogaRuntimeConfigSO Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!Resources.Load<YogaRuntimeConfigSO>("YogaRuntimeConfigSO"))
                {
                    return ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<YogaRuntimeConfigSO>(
                        YogaRuntimeConfigSOPath);
                }

                // Debug.Log("YOGA >>> Resources 加载运行时配置，确保路径可以使用");
                return Resources.Load<YogaRuntimeConfigSO>("YogaRuntimeConfigSO");
#else
                Resources.Load<YogaRuntimeConfigSO>("YogaRuntimeConfigSO")
#endif
            }
        }

        /// <summary>
        /// 运行模式
        /// </summary>
        public enum RuntimeMode
        {
            [LabelText("开发模式")]
            Debug,

            [LabelText("发布模式")]
            Release
        }

        [TitleGroup("运行模式", "发布模式不会显示内部信息")]
        [HideLabel]
        public RuntimeMode mode;

        [TitleGroup("模块站列表", "准备加载的模块站")]
        [AssetsOnly]
        [SerializeField]
        [HideLabel]
        List<AbstractContextSO> prepareToInitializeContexts;

        [DisableInEditorMode]
        [ShowInInspector]
        public ContextContainer contexts = new ContextContainer();

        public T GetContext<T>() where T : AbstractContextSO => contexts.Get<T>();

        public void Initialize()
        {
            InitializeContexts();
        }

        void InitializeContexts()
        {
            foreach (var contextSO in prepareToInitializeContexts)
            {
                contexts.Register(contextSO);
                contextSO.Init();
            }
        }
    }
}
