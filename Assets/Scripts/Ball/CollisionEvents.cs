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

        [TagProperty]
        [SerializeField] private string _spikeTag;

        [TagProperty]
        [SerializeField] private string _goalTag;

        private ContactPoint[] _contactPoints =  new ContactPoint[1];

		public event Action<ContactPoint> WallCollided;
        public event Action CoinCollided;
        public event Action<ContactPoint> SpikeCollided;
        public event Action<ContactPoint> GoalCollided;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag(_wallTag))
            {
                collision.GetContacts(_contactPoints);
                WallCollided.Invoke(_contactPoints[0]);
            }

            else if(collision.gameObject.CompareTag(_spikeTag))
            {
                collision.GetContacts(_contactPoints);
                SpikeCollided?.Invoke(_contactPoints[0]);
            }

            else if(collision.gameObject.CompareTag(_goalTag))
            {
                collision.GetContacts(_contactPoints);
                GoalCollided?.Invoke(_contactPoints[0]);
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