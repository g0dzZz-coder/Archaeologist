﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Archaeologist.UI
{
    using Core;
    using Gameplay;
    using Utils;

    public class UIManager : MonoBehaviour
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

        public void HideLastPanel()
        {
            if (lastPanel == null)
                return;

            HidePanel(lastPanel);
        }

        private void ShowEndMatchPanel(bool win)
        {
            if (win)
                ShowPanel(victoryPanel);
            else
                ShowPanel(defeatPanel);
        }

        private void ShowPanel(GameObject panel)
        {
            if (panel == null)
                return;

            if (panel.TryGetComponent(out Image image))
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
                image.raycastTarget = true;
                image.DOFade(1f, animationDuration);
            }

            panel.SetActive(true);
            lastPanel = panel;
        }

        private void HidePanel(GameObject panel)
        {
            if (panel == null)
                return;

            if (panel.TryGetComponent(out Image image))
            {
                image.DOFade(0f, animationDuration).onComplete += () => panel.SetActive(false);
                image.raycastTarget = false;
            }

            panel.SetActive(false);
        }

        private void RotateRestartButton(float duration)
        {
            var endValue = new Vector3(0, 0, imageButtonRestart.transform.rotation.eulerAngles.z - 720);
            imageButtonRestart.transform.DORotate(endValue, duration);
        }
    }
}