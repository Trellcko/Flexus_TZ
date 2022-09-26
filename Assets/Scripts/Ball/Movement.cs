using System;
using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour
	{
		[SerializeField] private DraggingPanel _draggingPanel;
        [SerializeField] private CollisionHandler _collisionHandler;
        [SerializeField] private float _mass = 2f;
        [SerializeField] private AnimatorController _bounceReceiver;
        [SerializeField] private ParticleSystem _dustEffect;

        private Rigidbody _rigidbody;
        
        private Vector3 _lastVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _draggingPanel.DragEnded += OnDragBegined;
            _collisionHandler.WallCollided += OnWallColided;
        }

        private void FixedUpdate()
        {
            _lastVelocity = _rigidbody.velocity;
        }
        private void OnDisable()
        {
            _draggingPanel.DragEnded -= OnDragBegined;
            _collisionHandler.WallCollided -= OnWallColided;
        }

        private void OnDragBegined()
        {
            Vector3 force = (_draggingPanel.StartPosition - _draggingPanel.CurrentPosition) / 10;
            force.z = force.y;
            force.y = 0;
            AddForce(force);
        }

        private void OnWallColided(ContactPoint contactPoint)
        {
            Reflect(contactPoint.normal);
            _dustEffect.transform.position = contactPoint.point;
            _dustEffect.transform.rotation = Quaternion.LookRotation(contactPoint.normal);
            _dustEffect.Play();
        }

        private void Reflect(Vector3 normal)
        {
            float speed = _lastVelocity.magnitude;
            Vector3 direct = Vector3.Reflect(_lastVelocity.normalized, normal);
            _rigidbody.velocity = direct * speed;
            _bounceReceiver.PlayBounchingAnimation(_rigidbody.velocity.magnitude, normal);
        }

        private void AddForce(Vector3 force)
        {
            _rigidbody.velocity += force / _mass;
        }
    }
}