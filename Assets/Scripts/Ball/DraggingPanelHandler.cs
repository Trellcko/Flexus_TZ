using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	public class DraggingPanelHandler : MonoBehaviour
	{
		[SerializeField] private DraggingPanel _draggingPanel;
		[SerializeField] private Movement _movement;

        private void OnEnable()
        {
            _draggingPanel.DragEnded += OnDragEnded;
        }

        private void OnDragEnded()
        {
            Vector3 force = (_draggingPanel.StartPosition - _draggingPanel.CurrentPosition) / 10;
            force.z = force.y;
            force.y = 0;
            _movement.AddForce(force);
        }
    }
}