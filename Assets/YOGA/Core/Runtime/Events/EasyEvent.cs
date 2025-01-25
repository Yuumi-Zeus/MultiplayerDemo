using System;

namespace YOGA.Core.Events
{
    public interface IEasyEvent
    {
        IRemoveListener AddListener(Action onEventNoArgs);
        void Clear();
    }

    /// <summary>
    /// 内存中运行的事件
    /// </summary>
    public class EasyEvent : IEasyEvent
    {
        public IRemoveListener AddListener(Action onEventNoArgs)
        {
            OnEvent += onEventNoArgs;
            return new RemoveListenerStruct(() => OnEvent -= onEventNoArgs);
        }

        public void Clear()
        {
            OnEvent = null;
        }

        event Action OnEvent;

        public void RemoveListener(Action onEventNoArgs)
        {
            OnEvent -= onEventNoArgs;
        }

        public void Invoke()
        {
            OnEvent?.Invoke();
        }
    }

    /// <summary>
    /// 内存中运行的事件
    /// </summary>
    public class EasyEvent<T> : IEasyEvent
    {
        public IRemoveListener AddListener(Action onEventNoArgs)
        {
            return AddListener(Event);

            void Event(T obj)
            {
                onEventNoArgs();
            }
        }

        public void Clear()
        {
            OnEvent = null;
        }

        event Action<T> OnEvent;

        public IRemoveListener AddListener(Action<T> onEvent)
        {
            OnEvent += onEvent;
            return new RemoveListenerStruct(() => OnEvent -= onEvent);
        }

        public void RemoveListener(Action<T> onEvent)
        {
            OnEvent -= onEvent;
        }

        public void Invoke(T e)
        {
            OnEvent?.Invoke(e);
        }
    }
}
