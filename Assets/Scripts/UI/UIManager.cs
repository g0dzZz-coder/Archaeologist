using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Archaeologist.UI
{
    using Core;

    public class UIManager : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] TextMeshProUGUI textScore = null;
        [SerializeField] TextMeshProUGUI textHearts = null;

        [Header("Panels")]
        [SerializeField] GameObject pausePanel = null;
        [SerializeField] GameObject winPanel = null;
        [SerializeField] GameObject losePanel = null;

        [Header("Scenes")]
        [SerializeField] string nameMenuScene = "Menu";

        [Header("Other")]
        [SerializeField] float durationAnim = 0.2f;
        [SerializeField] GameObject imageButtonRestart = null;

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                Pause();
        }

        public void Init(int startLives, int startScore)
        {
            pausePanel.SetActive(false);
            winPanel.SetActive(false);
            losePanel.SetActive(false);

            UpdateStatsUI(startLives, startScore);
        }

        public void UpdateStatsUI(int newHearts, int newScore)
        {
            textHearts.text = newHearts.ToString();
            textScore.text = newScore.ToString();
        }

        public void Pause()
        {
            ShowPanel(pausePanel);

            if (Game.IsPaused)
            {
                UnPause();
                return;
            }

            Game.Pause();
        }

        public void UnPause()
        {
            StartCoroutine(HidePanel(pausePanel));

            Game.UnPause();
        }

        public void Restart()
        {
            RotateRestartButton(durationAnim);

            Game.RestartGame();
        }

        public void ShowGameOverPanel(bool win)
        {
            if (win)
                ShowPanel(winPanel);
            else
                ShowPanel(losePanel);
        }

        public void BackToMenu()
        {
            UnPause();

            Game.LoadScene(nameMenuScene);
        }

        private void RotateRestartButton(float duration)
        {
            var endValue = new Vector3(0, 0, imageButtonRestart.transform.rotation.eulerAngles.z - 720);
            imageButtonRestart.transform.DORotate(endValue, duration);
        }

        private void ShowPanel(GameObject panel)
        {
            panel.SetActive(true);
            DOTween.Sequence()
                .Append(panel.GetComponent<Image>().DOFade(1f, durationAnim));
        }

        private IEnumerator HidePanel(GameObject panel)
        {
            DOTween.Sequence()
                   .Append(panel.GetComponent<Image>().DOFade(0f, durationAnim));

            yield return new WaitForSeconds(0f);

            panel.SetActive(false);
        }
    }
}