using UnityEngine;

namespace Trell.Flexus_TZ.Coin
{
	[AddComponentMenu("Coin Collision Handler")]
	public class CollisionHandler : MonoBehaviour
	{
		[SerializeField] private CollisionEvents _collisionEvents;
		[SerializeField] private AnimatorController _animatorController;


        private void OnEnable()
        {
            _collisionEvents.PlayerCollided += OnPlayerCollided;
        }

        private void OnDisable()
        {
            _collisionEvents.PlayerCollided -= OnPlayerCollided;
        }

        private void OnPlayerCollided()
        {
            _animatorController.PlayDiedAnimation();
        }
    }
}