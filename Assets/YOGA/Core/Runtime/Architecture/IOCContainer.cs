using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace YOGA.Core.Architecture
{
    /// <summary>
    /// 模块容器抽象类，用于存储和管理
    /// </summary>
    /// <typeparam name="TModule"> 模块接口 </typeparam>
    [Serializable]
    public abstract class IOCContainer<TModule>
    {
        /// <summary>
        /// 存储模块实例的字典
        /// </summary>
        [ShowInInspector]
        readonly Dictionary<Type, TModule> _modulesDict = new Dictionary<Type, TModule>();

        /// <summary>
        /// 注册一个模块实例
        /// </summary>
        /// <typeparam name="T"> 模块的类型，必须继承自TModule </typeparam>
        /// <param name="instance"> 要注册的模块实例 </param>
        public void Register<T>(T instance) where T : TModule
        {
            var key = typeof(T);
            // 如果该类型已存在，则不进行替换
            _modulesDict.TryAdd(key, instance);
        }

        /// <summary>
        /// 尝试获取一个模块实例
        /// </summary>
        /// <typeparam name="T"> 模块的类型，必须是TModule的子类 </typeparam>
        /// <returns> 如果找到，则返回该模块实例；否则，返回 null </returns>
        public T Get<T>() where T : class, TModule
        {
            var key = typeof(T);
            if (_modulesDict.TryGetValue(key, out var module))
            {
                return module as T;
            }

            return null;
        }

        /// <summary>
        /// 清除所有模块实例
        /// </summary>
        public void Clear()
        {
            _modulesDict.Clear();
        }
    }

    [Serializable]
    public class ModelContainer : IOCContainer<IModel> { }

    [Serializable]
    public class ContextContainer : IOCContainer<IContext> { }
}
