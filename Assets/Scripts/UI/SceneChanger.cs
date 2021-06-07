using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archaeologist.UI
{
    using Core;

    public class SceneChanger : MonoBehaviour
    {
        private static string SceneToLoad = string.Empty;

        [SerializeField] CanvasGroup canvasGroup = null;
        [Range(0f, 2f)]
        [SerializeField] float duration = 0.5f;

        private void Awake()
        {
            PlayFadeInAnimation();
        }

        private void OnEnable()
        {
            Game.SceneChanging += FadeToLevel;
        }

        private void OnDisable()
        {
            Game.SceneChanging -= FadeToLevel;
        }

        public void FadeToLevel(string newScene)
        {
            SceneToLoad = newScene;
            PlayFadeOutAnimation();
        }

        public void OnFadeComplete()
        {
            if (string.IsNullOrWhiteSpace(SceneToLoad))
                return;

            SceneManager.LoadSceneAsync(SceneToLoad);
        }

        private void PlayFadeInAnimation()
        {
            if (canvasGroup == null)
                return;

            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0f, duration);
        }

        private void PlayFadeOutAnimation()
        {
            if (canvasGroup == null)
                return;

            canvasGroup.DOFade(1f, duration).OnComplete(() => OnFadeComplete());
        }

        private void OnValidate()
        {
            if (canvasGroup == null)
                Debug.LogError($"[{nameof(SceneChanger)}] CanvasGroup is empty!");
        }
    }
}
