using System;
using UnityEditor;
using UnityEngine;

namespace Archaeologist.Core
{
    using UI;

    public static class Game
    {
        public static bool IsPaused { get; private set; }
        public static bool IsGameOver { get; private set; }

        public static Action SceneChanged { get; set; }

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            LoadScene("Menu");
            UnPause();
        }

        public static void GameOver()
        {
            IsGameOver = true;
        }

        public static void RestartGame()
        {
            UnPause();
        }

        public static void UnPause()
        {
            Time.timeScale = 1;
            IsPaused = false;
            IsGameOver = false;
        }

        public static void Pause()
        {
            Time.timeScale = 0;
            IsPaused = true;
        }

        public static void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif

            Application.Quit();
        }

        public static void LoadScene(string nameScene)
        {
            if (string.IsNullOrWhiteSpace(nameScene))
                return;

            SceneChanger.FadeToLevel(nameScene);

            SceneChanged?.Invoke();
        }
    }
}