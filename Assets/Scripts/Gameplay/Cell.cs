using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Archaeologist.Gameplay
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class Cell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] int maxDepth = 3;
        [SerializeField] float animationDuration = 1f;

        [Header("Colors")]
        [SerializeField] Color groundColor = Color.green;
        [SerializeField] Color depthColor = Color.black;

        public bool IsEmpty => depth < 1;

        public event Action<Cell> Clicked;

        private int depth;
        private Image image = null;
        private Button button = null;

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();

            depth = maxDepth;
            UpdateCell();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }

        public void Dig()
        {
            depth--;
            UpdateCell();
        }

        private void UpdateCell()
        {
            var newColor = Color.Lerp(depthColor, groundColor, depth / (float)maxDepth);
            //image.color = newColor;
            image.DOColor(newColor, animationDuration);

            button.interactable = !IsEmpty;
        }
    }
}