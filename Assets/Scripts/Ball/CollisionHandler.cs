using System;
using Trell.Flexus_TZ.Core;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	public class CollisionHandler : MonoBehaviour
	{
		[TagProperty]
		[SerializeField] private string _wallTag;

        private ContactPoint[] _contactPoints =  new ContactPoint[1];

		public event Action<ContactPoint> WallCollided;

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag(_wallTag))
            {
                collision.GetContacts(_contactPoints);
                WallCollided.Invoke(_contactPoints[0]);
            }
        }
    }
}