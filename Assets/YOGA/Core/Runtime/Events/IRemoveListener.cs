using UnityEngine;

namespace YOGA.Core.Events
{
    public interface IRemoveListener
    {
        void RemoveListener();
    }

    public static class RemoveListenerExtension
    {
        static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
        {
            var invoker = gameObject.GetComponent<T>();

            if (!invoker)
            {
                invoker = gameObject.AddComponent<T>();
            }

            return invoker;
        }

        public static void OnDisableRemoveListener<T>(this IRemoveListener self, T component) where T : Component
        {
            OnDisableRemoveListener(self, component.gameObject);
        }

        public static void OnDestroyRemoveListener<T>(this IRemoveListener self, T component) where T : Component
        {
            OnDestroyRemoveListener(self, component.gameObject);
        }

        public static void OnDisableRemoveListener(this IRemoveListener self, GameObject obj)
        {
            GetOrAddComponent<RemoveListenerTrigger>(obj)
                .AddToDisableList(self);
        }

        public static void OnDestroyRemoveListener(this IRemoveListener self, GameObject obj)
        {
            GetOrAddComponent<RemoveListenerTrigger>(obj)
                .AddToDestroyList(self);
        }
    }
}
