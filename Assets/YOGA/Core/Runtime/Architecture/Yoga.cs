using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YOGA.Core.Config;
using YOGA.Core.Events;
using YOGA.OdinToolkits;

namespace YOGA.Core.Architecture
{
    /// <summary>
    /// Yoga 是框架的核心类，也是框架的入口
    /// </summary>
    [DefaultExecutionOrder(-200)]
    public partial class Yoga :
#if UNITY_EDITOR
        SerializedMonoBehaviour
#else
        MonoBehaviour
#endif
    {
        public static Yoga Instance { get; private set; }

        public YogaRuntimeConfigSO runtimeConfig;

        [PropertyOrder(-10)]
        [DisplayAsString(FontSize = 15)]
        [HideLabel]
        public string warning = "禁止手动添加 Yoga 脚本到场景物体上";

        void OnDestroy()
        {
            Instance = null;
            ClearAllBuses();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void YogaInitialize()
        {
            // 强制生成 Yoga 实例
            Instance = new GameObject("[Yoga]")
                .AddComponent<Yoga>();
            DontDestroyOnLoad(Instance.gameObject);
            // 读取 YogaRuntimeConfigSO
            Instance.runtimeConfig = YogaRuntimeConfigSO.Instance;
            Instance.runtimeConfig.Initialize();
            // 初始化事件总线，收集所有 EventArgs 类型
            EventTypes = PredefinedAssemblyUtility.GetRuntimeTypesWithInterface(typeof(IEventArgs));
            var typedef = typeof(EventBus<>);
            _eventBusTypes = EventTypes
                .Select(eventType => typedef.MakeGenericType(eventType)).ToList();
        }

        public static T GetContext<T>() where T : AbstractContextSO => Instance.runtimeConfig.GetContext<T>();
    }

    public partial class Yoga
    {
        [ShowInInspector]
        [PropertyOrder(10)]
        [HideLabel]
        [TitleGroup("项目中的事件参数类型", "一个参数代表一个全局事件")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        public static IReadOnlyList<Type> EventTypes;

        static IReadOnlyList<Type> _eventBusTypes;

        static void ClearAllBuses()
        {
            foreach (var clearMethod in _eventBusTypes
                         .Select(busType => busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic)))
            {
                clearMethod?.Invoke(null, null);
                if (clearMethod != null && clearMethod.DeclaringType != null)
                {
                    Debug.Log($"Cleared EventBus<{clearMethod.DeclaringType.GetGenericArguments()[0].Name}>");
                }
            }
        }
    }
}
