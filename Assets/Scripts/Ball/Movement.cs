using System;
using Trell.Flexus_TZ.UI;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour
	{
        [Min(1)]
        [SerializeField] private float _mass = 2f;
        [Min(0)]
        [SerializeField] private float _drag;
        [Min(0.1f)]
        [SerializeField] private float _radius = 0.5f;

        private Vector3 _velocity;

        public float Speed => _velocity.magnitude;

        public Vector3 Direct => _velocity.normalized;
        
        private void FixedUpdate()
        {
            Vector3 deltaPosition = _velocity * Time.fixedDeltaTime + _velocity * Time.fixedDeltaTime;

            transform.position += deltaPosition;
            _velocity *= Mathf.Clamp01(1 - _drag * Time.fixedDeltaTime);

            Vector3 deltaPositionForRotation = new Vector3(deltaPosition.z, 0, -deltaPosition.x);

            float angel = deltaPositionForRotation.magnitude * (180f / Mathf.PI) / _radius;
            transform.rotation = Quaternion.Euler(deltaPositionForRotation.normalized * angel) * transform.rotation;
        }


        public void Reflect(Vector3 normal)
        {
            _velocity = Vector3.Reflect(_velocity, normal);        }

        public void AddForce(Vector3 force)
        {
            _velocity += force;
        }
    }
}