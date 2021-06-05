using System;

namespace Archaeologist.Gameplay
{
    public static class Player
    {
        public static int Shovels { get; private set; }
        public static int Score { get; private set; }

        public static Action<int> ShovelUpdated { get; set; }
        public static Action<int> ScoreUpdated { get; set; }

        public static void IncreaseScore(int amount)
        {
            SetScore(Score + amount);
        }

        public static void AddShovels()
        {
            SetShovels(Shovels + 1);
        }

        public static void RemoveShovel()
        {
            if (Shovels < 1)
            {
                ShovelUpdated?.Invoke(0);
                return;
            }

            SetShovels(Shovels - 1);
        }

        public static void ResetStats(int shovelsAmount)
        {
            SetScore(0);
            SetShovels(shovelsAmount);
        }

        private static void SetScore(int amount)
        {
            if (amount < 0)
                throw new ArgumentException();

            Score = amount;

            ScoreUpdated?.Invoke(Score);
        }

        private static void SetShovels(int amount)
        {
            if (amount < 0)
                throw new ArgumentException();

            Shovels = amount;

            ShovelUpdated?.Invoke(Shovels);
        }
    }
}