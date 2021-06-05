using System;
using UnityEngine;

namespace Archaeologist.Gameplay
{
    public class MatchController : MonoBehaviour
    {
        [SerializeField] int startShovelsAmount = 20;
        [SerializeField] int scoreToWin = 3;

        public event Action MatchRestarted;
        public event Action<bool> MatchEnded;

        private void Awake()
        {
            MatchRestarted += OnMatchRestarted;

            Player.ScoreUpdated += OnScoreUpdated;
            Player.ShovelUpdated += OnShovelsUpdated;

            MatchRestarted?.Invoke();
        }

        public void Restart()
        {
            MatchRestarted?.Invoke();
        }
        
        private void Finish(bool win = false)
        {
            MatchEnded?.Invoke(win);
        }

        private void OnMatchRestarted()
        {
            Player.ResetStats(startShovelsAmount);
        }

        private void OnScoreUpdated(int score)
        {
            if (score >= scoreToWin)
                Finish(true);
        }

        private void OnShovelsUpdated(int count)
        {
            if (count < 1)
                Finish(false);
        }
    }
}