using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	public class Movement : MonoBehaviour
	{
		[SerializeField] private DraggingPanel _draggingPanel;


        private void OnEnable()
        {
            _draggingPanel.DragBegined += OnDragBegined;
        }

        private void OnDragBegined()
        {

        }
    }
}