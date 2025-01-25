using System;

namespace YOGA.Core.Events
{
    /// <summary>
    /// 存储一个移除监听的方法
    /// </summary>
    public struct RemoveListenerStruct : IRemoveListener
    {
        Action _removeMethod;
        public RemoveListenerStruct(Action removeMethod) => _removeMethod = removeMethod;

        public void RemoveListener()
        {
            _removeMethod?.Invoke();
            _removeMethod = null;
        }
    }
}
