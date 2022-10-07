using System;
using Trell.Flexus_TZ.Core.Pause;
using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour, IPauseHandler
	{
        [Min(0)]
        [SerializeField] private float _drag;
        [Min(0.1f)]
        [SerializeField] private float _radius = 0.5f;

        private Vector3 _velocity;

        public float Speed => _velocity.magnitude;

        public Vector3 Direct => _velocity.normalized;

        private int _pauseModifier = 1;

        private void Awake()
        {
            PauseManager.Instance.Subscribe(this);
        }

        private void FixedUpdate()
        {
            Vector3 deltaPosition = Move();

            CalculateVelocity();

            if (PauseManager.Instance.IsPaused == false)
            {
                RotateInDirection(deltaPosition);
            }
        }

        private void CalculateVelocity()
        {
            if (PauseManager.Instance.IsPaused == false)
            {
                _velocity *= Mathf.Clamp01(1 - _drag * Time.fixedDeltaTime);
            }
            
        }

        private void RotateInDirection(Vector3 deltaPosition)
        {
            Vector3 deltaPositionForRotation = new Vector3(deltaPosition.z, 0, -deltaPosition.x);

            float angel = deltaPositionForRotation.magnitude * (180f / Mathf.PI) / _radius;

            transform.rotation = Quaternion.Euler(deltaPositionForRotation.normalized * angel) * transform.rotation;
        }

        private Vector3 Move()
        {
            Vector3 deltaPosition = _velocity * Time.fixedDeltaTime + _velocity * Time.fixedDeltaTime;

            deltaPosition *= _pauseModifier;

            transform.position += deltaPosition;
            return deltaPosition;
        }

        public void Reflect(Vector3 normal)
        {
            _velocity = Vector3.Reflect(_velocity, normal);        
        }

        public void VelocityChange(Vector3 velocity)
        {
            _velocity = velocity;
        }

        public void AddForce(Vector3 force)
        {
            _velocity += force;
        }

        public void OnPause()
        {
            _pauseModifier = 0;
        }

        public void OnUnPause()
        {
            _pauseModifier = 1;
        }
    }
}