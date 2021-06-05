using TMPro;
using UnityEngine;

namespace Archaeologist.UI
{
    using Gameplay;

    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText = null;
        [SerializeField] TMP_Text shovelsText = null;

        private void Start()
        {
            UpdateScore(Player.Score);
            UpdateShovels(Player.Shovels);
        }

        private void OnEnable()
        {
            Player.ScoreUpdated += UpdateScore;
            Player.ShovelUpdated += UpdateShovels;
        }

        private void OnDisable()
        {
            Player.ScoreUpdated -= UpdateScore;
            Player.ShovelUpdated -= UpdateShovels;
        }

        private void UpdateScore(int amount)
        {
            if (scoreText == null)
                return;

            scoreText.text = amount.ToString();
        }

        private void UpdateShovels(int amount)
        {
            if (shovelsText == null)
                return;

            shovelsText.text = amount.ToString();
        }
    }
}