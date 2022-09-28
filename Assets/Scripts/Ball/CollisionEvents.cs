using System;
using Trell.Flexus_TZ.Core;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [AddComponentMenu("Ball Collision Events")]
	public class CollisionEvents : MonoBehaviour
	{
		[TagProperty]
		[SerializeField] private string _wallTag;

        [TagProperty]
        [SerializeField] private string _coinTag;

        private ContactPoint[] _contactPoints =  new ContactPoint[1];

		public event Action<ContactPoint> WallCollided;
        public event Action CoinCollided;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag(_wallTag))
            {
                collision.GetContacts(_contactPoints);
                WallCollided.Invoke(_contactPoints[0]);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.CompareTag(_coinTag))
            {
                CoinCollided?.Invoke();
            }
        }
    }
}