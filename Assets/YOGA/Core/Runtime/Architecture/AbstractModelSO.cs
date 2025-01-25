using UnityEngine;

namespace YOGA.Core.Architecture
{
    public interface IModel
    {
        IContext Station { get; set; }
        void Init(IContext station);
    }

    public abstract class AbstractModelSO : ScriptableObject, IModel
    {
        public IContext Station { get; set; }

        public void Init(IContext station)
        {
            Station = station;
        }
    }
}
