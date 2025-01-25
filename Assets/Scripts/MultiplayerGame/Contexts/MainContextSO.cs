using UnityEngine;
using YOGA.Core.Architecture;

namespace MultiplayerGame.Contexts
{
    public class MainContextSO : AbstractContextSO
    {
        public GridDataSO gridData;

        public override void Init()
        {
            base.Init();
            gridData = Resources.Load<GridDataSO>("DataSO/GridDataSO");
        }
    }
}
