using DG.Tweening;
using UnityEngine;

namespace Archaeologist.UI
{
    using Core;
    using Gameplay;
    using Utils;

    public class GameUI : MonoBehaviour
    {
        [SerializeField] MatchController matchController = null;
        [SerializeField] GameObject victoryPanel = null;
        [SerializeField] GameObject defeatPanel = null;
        [SerializeField] GameObject imageButtonRestart = null;
        [Range(0f, 2f)]
        [SerializeField] float animationDuration = 0.3f;

        private GameObject lastPanel;

        private void Awake()
        {
            victoryPanel.SetActive(false);
            defeatPanel.SetActive(false);

            matchController.MatchEnded += ShowEndMatchPanel;
        }

        public void Restart()
        {
            HideLastPanel();
            RotateRestartButton(animationDuration);

            matchController.Restart();
        }

        public void BackToMenu()
        {
            Game.LoadScene(SceneNames.Menu);
        }

        private void HideLastPanel()
        {
            if (lastPanel == null)
                return;

            HidePanel(lastPanel);
        }

        private void ShowEndMatchPanel(bool win)
        {
            ShowPanel(win ? victoryPanel : defeatPanel);
        }

        private void ShowPanel(GameObject panel)
        {
            if (panel == null)
                return;

            if (panel.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, animationDuration);
                canvasGroup.interactable = true;
            }

            panel.SetActive(true);
            lastPanel = panel;
        }

        private void HidePanel(GameObject panel)
        {
            if (panel == null)
                return;

            if (panel.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup.DOFade(0f, animationDuration).OnComplete(() => panel.SetActive(false));
                canvasGroup.interactable = false;
            }
        }

        private void RotateRestartButton(float duration)
        {
            var endValue = new Vector3(0, 0, imageButtonRestart.transform.rotation.eulerAngles.z - 720);
            imageButtonRestart.transform.DORotate(endValue, duration);
        }
    }
}