using UnityEngine;
using UnityEngine.EventSystems;

namespace Archaeologist.UI
{
    public class Link : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] string url = string.Empty;

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenUrl();
        }

        public void OpenUrl()
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            Application.OpenURL(url);
        }
    }
}