using Trell.Flexus_TZ.Core.Pause;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Trell.Flexus_TZ.UI
{
    [RequireComponent(typeof(Image))]
	public class PauseButton : MonoBehaviour, IPointerClickHandler
	{
        [SerializeField] private PausePanel _pausePanel;

        public void OnPointerClick(PointerEventData eventData)
        {
            _pausePanel.gameObject.SetActive(true);
            PauseManager.Instance.Pause();
        }
    }
}