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

        public float Speed => _rigidbody.velocity.magnitude;

        public Vector3 Direct => _rigidbody.velocity.normalized;

        private Rigidbody _rigidbody;
        
        private Vector3 _lastVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _lastVelocity = _rigidbody.velocity;
        }

        public void Reflect(Vector3 normal)
        {
            float speed = _lastVelocity.magnitude;
            Vector3 direct = Vector3.Reflect(_lastVelocity.normalized, normal);
            _rigidbody.velocity = direct * speed;
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.velocity += force / _mass;
        }
    }
}