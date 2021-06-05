using UnityEngine;

namespace Archaeologist.UI.Menu
{
    using Core;

    public class Menu : MonoBehaviour
    {
        [SerializeField] GameObject exitPanel = null;

        [Header("Scenes")]
        [SerializeField] string nameSeneGame = "Game";
        [SerializeField] string nameSceneAbout = "About";

        private void Awake()
        {
            if (exitPanel)
                exitPanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Debug.Log(1);
                OnCancenButtonPressed();
            }
        }

        public void StartGame()
        {
            Game.LoadScene(nameSeneGame);
        }

        public void ShowAbout()
        {
            Game.LoadScene(nameSceneAbout);
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