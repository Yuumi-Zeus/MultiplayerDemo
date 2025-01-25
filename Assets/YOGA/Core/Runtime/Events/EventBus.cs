using System;
using System.Collections.Generic;

namespace YOGA.Core.Events
{
    /// <summary>
    /// 特定类型为参数的事件站，允许有多个 InspectorEvent&lt;T&gt; 实例对象被不同的对象所持有
    /// </summary>
    public static class EventBus<T> where T : IEventArgs
    {
        static readonly HashSet<IInspectorEvent<T>> EventSet = new HashSet<IInspectorEvent<T>>();

        /// <summary>
        /// 默认事件，直接使用 EventBus 监听事件时，会使用默认事件
        /// </summary>
        static InspectorEvent<T> _defaultEvent;

        public static IRemoveListener AddListener(Action onEventNoArgs)
        {
            return EnsureDefaultEvent().AddListener(onEventNoArgs);
        }

        public static IRemoveListener AddListener(Action<T> onEvent)
        {
            return EnsureDefaultEvent().AddListener(onEvent);
        }

        public static void RemoveListener(Action onEventNoArgs)
        {
            EnsureDefaultEvent().RemoveListener(onEventNoArgs);
        }

        public static void RemoveListener(Action<T> onEvent)
        {
            EnsureDefaultEvent().RemoveListener(onEvent);
        }

        public static void CollectEvent(InspectorEvent<T> inspectorEvent)
        {
            EventSet.Add(inspectorEvent);
        }

        public static void DiscardEvent(InspectorEvent<T> inspectorEvent)
        {
            EventSet.Remove(inspectorEvent);
        }

        public static void Invoke(T e)
        {
            // 获取 EventSet 的快照，以避免在事件处理过程中修改 EventSet 导致迭代异常
            var snapshot = new HashSet<IInspectorEvent<T>>(EventSet);

            foreach (var inspectorEvent in snapshot)
            {
                try
                {
                    inspectorEvent.Invoke(e);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error invoking event: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 自动控制全局 Clear
        /// </summary>
        static void Clear()
        {
            var eventSetCopy = new List<IInspectorEvent<T>>(EventSet);
            foreach (var @event in eventSetCopy)
            {
                @event.Clear();
            }

            EventSet.Clear();
        }

        static InspectorEvent<T> EnsureDefaultEvent()
        {
            if (_defaultEvent != null)
            {
                return _defaultEvent;
            }

            _defaultEvent = new InspectorEvent<T>();
            CollectEvent(_defaultEvent);
            return _defaultEvent;
        }
    }
}
