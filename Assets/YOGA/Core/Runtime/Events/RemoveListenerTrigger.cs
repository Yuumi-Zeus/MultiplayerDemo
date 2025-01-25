using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace YOGA.Core.Events
{
    [HideMonoScript]
    public class RemoveListenerTrigger : MonoBehaviour
    {
        [ShowInInspector]
        [LabelText("OnDestroy 移除监听数量")]
        [ListDrawerSettings(ShowFoldout = true, HideAddButton = true)]
        readonly List<IRemoveListener> _destroyList = new List<IRemoveListener>();

        [ShowInInspector]
        [LabelText("OnDisable 移除监听数量")]
        [ListDrawerSettings(ShowFoldout = true, HideAddButton = true)]
        readonly List<IRemoveListener> _disableList = new List<IRemoveListener>();

        void OnDisable()
        {
            foreach (var listener in _disableList)
            {
                listener.RemoveListener();
            }

            _disableList.Clear();
        }

        void OnDestroy()
        {
            foreach (var listener in _destroyList)
            {
                listener.RemoveListener();
            }

            _destroyList.Clear();
        }

        public void AddToDisableList(IRemoveListener removeListener)
        {
            _disableList.Add(removeListener);
        }

        public void AddToDestroyList(IRemoveListener removeListener)
        {
            _destroyList.Add(removeListener);
        }
    }
}
