using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	public class DraggingPanelHandler : MonoBehaviour
	{
		[SerializeField] private DraggingPanel _draggingPanel;

        [Range(0.01f, 0.1f)]
        [SerializeField] private float _draginPanelMultiplayer;
		[SerializeField] private Movement _movement;

        private void OnEnable()
        {
            _draggingPanel.DragEnded += OnDragEnded;
        }

        private void OnDragEnded()
        {
            Vector3 force = (_draggingPanel.StartPosition - _draggingPanel.CurrentPosition) * _draginPanelMultiplayer;
            force.z = force.y;
            force.y = 0;
            _movement.AddForce(force);
        }
    }
}