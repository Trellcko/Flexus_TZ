using Trell.Flexus_TZ.Core.Pause;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Trell.Flexus_TZ.UI
{
    public class PausePanel : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            PauseManager.Instance.UnPause();
            gameObject.SetActive(false);
        }
    }
}