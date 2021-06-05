using DG.Tweening;
using UnityEngine;

namespace Archaeologist.Game
{
    using Core;

    public class Tile : MonoBehaviour
    {
        [SerializeField] TileType type;
        [SerializeField] float animationTime = 1f;

        public int PosX { get; set; }
        public int PosY { get; set; }

        public bool InUse { get; set; }

        private SpriteRenderer ballColorRender;

        private void Awake()
        {
            ballColorRender = GetComponent<SpriteRenderer>();

            Explode(0);
        }

        private void OnMouseUp()
        {
            if (Game.IsPaused || Game.IsGameOver)
                return;
        }

        public void Init(TileType type)
        {
            this.type = type;

            InUse = true;

            ballColorRender.sprite = type.sprite;
            ballColorRender.color = type.color;

            DOTween.Sequence()
                   .Append(transform.DOScale(1f, animationTime))
                   .Join(ballColorRender.DOFade(1f, animationTime));
        }

        public void Explode(float time)
        {
            InUse = false;
            DOTween.Sequence()
                   .Append(transform.DOScale(0f, time))
                   .Join(ballColorRender.DOFade(0f, time));
        }
    }
}