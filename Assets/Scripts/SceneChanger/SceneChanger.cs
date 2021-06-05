using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archaeologist.UI
{
    [RequireComponent(typeof(Animator))]
    public class SceneChanger : MonoBehaviour
    {
        private static Animator Animator = null;
        private static string SceneToLoad = "";

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        public static void FadeToLevel(string newScene)
        {
            SceneToLoad = newScene;

            if (Animator == null)
                return;

            Animator.SetTrigger("FadeOut");
        }

        public void OnFadeComplete()
        {
            if (string.IsNullOrWhiteSpace(SceneToLoad))
                return;

            SceneManager.LoadSceneAsync(SceneToLoad);
        }
    }
}
