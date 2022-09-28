using System;
using Trell.Flexus_TZ.Core;
using UnityEngine;

namespace Trell.Flexus_TZ.Coin
{
	[AddComponentMenu("Coin Collision Events")]
	public class CollisionEvents : MonoBehaviour
	{
		[TagProperty]
		[SerializeField] private string _ballTag;

        public event Action BallCollided; 

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(_ballTag))
            {
                BallCollided?.Invoke();
            }
        }

    }
}