using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Visual
{
	public class ArrowDrawer : MonoBehaviour
	{
  		[SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private DraggingPanel _draggingPanel;

        private void OnEnable()
        {
            _draggingPanel.DragEnded += OnDragEnded;
        }

        private void OnDisable()
        {
            _draggingPanel.DragEnded -= OnDragEnded;
        }

        private void Update()
        {
            if(_draggingPanel.IsDragging)
            {
                Vector3 inverseDragDirection = _draggingPanel.StartPosition - _draggingPanel.CurrentPosition;
                inverseDragDirection.y = 0;

                SetLength(Vector3.Distance(_draggingPanel.CurrentPosition, _draggingPanel.StartPosition));
                
                LookAt(inverseDragDirection);
            }
        }

        private void OnValidate()
        {
            InitLineRender();
        }

        private void LookAt(Vector3 targetPosition)
        {
            transform.rotation = Quaternion.LookRotation(targetPosition);
        }

        private void SetLength(float length)
        {
            _lineRenderer.SetPosition(1, length * Vector3.forward);

        }
        
        private void OnDragEnded()
        {
            SetLength(0);
        }

        private void InitLineRender()
        {
            if (_lineRenderer)
            {
                _lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
                _lineRenderer.positionCount = 2;
            }
        }
    }
}