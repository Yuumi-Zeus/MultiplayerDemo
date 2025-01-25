using System;
using UnityEngine.Events;

namespace YOGA.Core.Events
{
    public interface IInspectorEvent : IEasyEvent
    {
        bool HasListener { get; }
        void RemoveListener(Action onEventNoArgs);
        int GetListenerCount();
    }

    public interface IInspectorEvent<T> : IInspectorEvent
    {
        IRemoveListener AddListener(Action<T> onEvent);
        void RemoveListener(Action<T> onEvent);
        void Invoke(T e);
    }

    /// <summary>
    /// Inspector 面板的可查看可配置的 Event 类型
    /// </summary>
    [Serializable]
    public class InspectorEvent : IInspectorEvent
    {
        public bool HasListener
        {
            get => OnEventNoArgs != null;
        }

        public IRemoveListener AddListener(Action onEventNoArgs)
        {
            OnEventNoArgs += onEventNoArgs;
            return new RemoveListenerStruct(() => OnEventNoArgs -= onEventNoArgs);
        }

        public void RemoveListener(Action onEventNoArgs)
        {
            OnEventNoArgs -= onEventNoArgs;
        }

        public void Clear()
        {
            OnEventNoArgs = null;
        }

        public int GetListenerCount()
        {
            var count = OnEventNoArgs?.GetInvocationList().Length ?? 0;
            return count;
        }

        event Action OnEventNoArgs;

        public void Invoke()
        {
            OnEventNoArgs?.Invoke();
        }
    }

    /// <summary>
    /// Inspector 面板的可查看可配置的 Event 类型
    /// </summary>
    [Serializable]
    public class InspectorEvent<T> : IInspectorEvent<T> where T : IEventArgs
    {
        public bool HasListener
        {
            get => OnEventNoArgs != null || OnEvent != null;
        }

        public IRemoveListener AddListener(Action onEventNoArgs)
        {
            OnEventNoArgs += onEventNoArgs;
            return new RemoveListenerStruct(() => OnEventNoArgs -= onEventNoArgs);
        }

        public IRemoveListener AddListener(Action<T> onEvent)
        {
            OnEvent += onEvent;
            return new RemoveListenerStruct(() => OnEvent -= onEvent);
        }

        public void RemoveListener(Action onEventNoArgs)
        {
            OnEventNoArgs -= onEventNoArgs;
        }

        public void RemoveListener(Action<T> onEvent)
        {
            OnEvent -= onEvent;
        }

        public void Invoke(T e)
        {
            OnEventNoArgs?.Invoke();
            OnEvent?.Invoke(e);
        }

        public void Clear()
        {
            OnEventNoArgs = null;
            OnEvent = null;
        }

        public int GetListenerCount()
        {
            var noArgsEventCount = OnEventNoArgs?.GetInvocationList().Length ?? 0;
            var onEventCount = OnEvent?.GetInvocationList().Length ?? 0;
            return noArgsEventCount + onEventCount;
        }

        event Action OnEventNoArgs;
        event Action<T> OnEvent;

        public InspectorEvent<T> Collected()
        {
            EventBus<T>.CollectEvent(this);
            return this;
        }
    }
}
