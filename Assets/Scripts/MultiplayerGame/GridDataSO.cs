using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MultiplayerGame
{
    public class GridDataSO : ScriptableObject
    {
        [ShowInInspector]
        public static List<GridPositionPresenter> GridPositionPresenters;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void FindAllGrid()
        {
            GridPositionPresenters = FindObjectsByType<GridPositionPresenter>(FindObjectsInactive.Include,
                    FindObjectsSortMode.None)
                .ToList();
            GridPositionPresenters.Sort(new GridCompare());
        }

        class GridCompare : IComparer<GridPositionPresenter>
        {
            //x 和 y，返回 0 相等，-1 小于， 1 大于
            public int Compare(GridPositionPresenter x, GridPositionPresenter y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }

                if (x == null)
                {
                    return -1;
                }

                if (y == null)
                {
                    return 1;
                }

                if (x.GridPosition.x < y.GridPosition.x)
                {
                    return -1;
                }

                if (x.GridPosition.x > y.GridPosition.x)
                {
                    return 1;
                }

                if (x.GridPosition.y < y.GridPosition.y)
                {
                    return -1;
                }

                if (x.GridPosition.y > y.GridPosition.y)
                {
                    return 1;
                }

                return 0;
            }
        }
    }
}
