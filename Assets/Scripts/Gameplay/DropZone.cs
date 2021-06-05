using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Archaeologist.Gameplay
{
    public class DropZone : MonoBehaviour
    {
        [SerializeField] GraphicRaycaster graphicRaycaster = null;

        public bool IsPointerReleasedOver(PointerEventData eventData)
        {
            var results = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject == gameObject)
                    return true;
            }

            return false;
        }
    }
}