using UnityEngine;
using UnityEngine.EventSystems;
using YOGA.Core.Architecture;

namespace MultiplayerGame
{
    public class GridPositionPresenter : Presenter, IPointerDownHandler
    {
        [SerializeField]
        int gridX;

        [SerializeField]
        int gridY;

        public Vector2 GridPosition => new Vector2(gridX, gridY);

        void OnMouseDown()
        {
            Debug.Log("Click" + gameObject.name);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown Click" + gameObject.name);
        }
    }
}
