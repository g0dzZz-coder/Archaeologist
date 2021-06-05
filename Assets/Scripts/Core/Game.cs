using System;
using UnityEditor;
using UnityEngine;

namespace Archaeologist.Core
{
    using UI;
    using Utils;

    public static class Game
    {
        public static event Action SceneChanged;

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            LoadScene(SceneNames.Menu);
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