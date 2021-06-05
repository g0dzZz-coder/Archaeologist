using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Archaeologist.Gameplay
{
    public class Treasure : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] int reward = 1;

        public int Reward => reward;

        public event Action<PointerEventData, Treasure> DragEnded;

        private RectTransform canvas;
        private Transform root;
        private Vector2 dragStartPosition;

        public void Init(RectTransform canvas, Transform treasureRoot)
        {
            this.canvas = canvas;
            root = treasureRoot;

            transform.SetParent(treasureRoot);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition = CameraToCanvasPosition(eventData.position);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragStartPosition = transform.localPosition;
            transform.SetParent(canvas);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(root);

            DragEnded?.Invoke(eventData, this);

            ResetPosition();
        }

        public void ResetPosition()
        {
            transform.localPosition = dragStartPosition;
        }

        private Vector2 CameraToCanvasPosition(Vector2 cameraPosition)
        {
            var position = cameraPosition /
                new Vector2(Screen.width, Screen.height) * canvas.sizeDelta - canvas.sizeDelta * 0.5f;

            return position;
        }
    }
}