using UnityEngine;

namespace Archaeologist.UI
{
    public class Link : MonoBehaviour
    {
        [SerializeField]
        private string url = "";

        public void OpenUrl()
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            Application.OpenURL(url);
        }
    }
}