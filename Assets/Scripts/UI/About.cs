using UnityEngine;

namespace Archaeologist.UI
{
    using Core;

    public class About : MonoBehaviour
    {
        [SerializeField] string nameMenuScene = "Menu";

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                BackToMenu();
        }

        public void BackToMenu()
        {
            Game.LoadScene(nameMenuScene);
        }
    }
}