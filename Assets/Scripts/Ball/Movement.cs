using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour
	{
		[SerializeField] private DraggingPanel _draggingPanel;
        [SerializeField] private float _mass = 2f;

        private Rigidbody _rigidbody;
        

        private float _velocityChange;
        private Vector3 _newVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _draggingPanel.DragEnded += OnDragBegined;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, _newVelocity, _velocityChange * Time.fixedDeltaTime);
            _newVelocity -= _newVelocity.normalized * Time.fixedDeltaTime;
        }

        private void OnDragBegined()
        {
            Vector3 force = (_draggingPanel.StartPosition - _draggingPanel.CurrentPosition);
            force.y = 0;
            AddForce(force);
        }

        private void AddForce(Vector3 force)
        {
            _newVelocity += force / _mass;
            _velocityChange = force.magnitude; 
        }
    }
}