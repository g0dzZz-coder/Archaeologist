using UnityEngine;

namespace Archaeologist.UI.Menu
{
    using Core;
    using Utils;

    public class Menu : MonoBehaviour
    {
        [SerializeField] GameObject exitPanel = null;

        private void Awake()
        {
            if (exitPanel)
                exitPanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                OnCancenButtonPressed();
        }

        public void StartGame()
        {
            Game.LoadScene(SceneNames.Game);
        }

        public void ShowAbout()
        {
            Game.LoadScene(SceneNames.About);
        }

        public void ToggleExitPanel()
        {
            exitPanel.SetActive(!exitPanel.activeSelf);
        }

        public void ConfirmExit()
        {
            Game.Exit();
        }

        private void OnCancenButtonPressed()
        {
            if (exitPanel.activeSelf)
                ConfirmExit();
            else
                ToggleExitPanel();
        }
    }
}