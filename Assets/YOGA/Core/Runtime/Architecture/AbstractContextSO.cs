using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace YOGA.Core.Architecture
{
    public interface IContext
    {
        void Init();
        T GetModel<T>() where T : class, IModel;
    }

    public abstract class AbstractContextSO : ScriptableObject, IContext
    {
        [TitleGroup("Model 列表", "准备加载的数据 Model")]
        [AssetsOnly]
        [SerializeField]
        [HideLabel]
        List<AbstractModelSO> prepareToInitializeModels;

        [ShowInInspector]
        [DisableInEditorMode]
        ModelContainer _modelContainer = new ModelContainer();

        public virtual void Init()
        {
            foreach (var model in prepareToInitializeModels)
            {
                _modelContainer.Register(model);
                model.Init(this);
            }
        }

        public T GetModel<T>() where T : class, IModel => _modelContainer.Get<T>();
    }
}
