using UnityEngine;

namespace Archaeologist.UI
{
    using Utils;
    using Core;

    public class About : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                BackToMenu();
        }

        public void BackToMenu()
        {
            Game.LoadScene(SceneNames.Menu);
        }
    }
}