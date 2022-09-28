using UnityEngine;

namespace Trell.Flexus_TZ.Coin
{
	[AddComponentMenu("Coin (Ball Collision Handler)")]
	public class BallCollisionHandler : MonoBehaviour
	{
		[SerializeField] private CollisionEvents _collisionEvents;
		[SerializeField] private AnimatorController _animatorController;


        private void OnEnable()
        {
            _collisionEvents.BallCollided += OnBallCollided;
        }

        private void OnDisable()
        {
            _collisionEvents.BallCollided -= OnBallCollided;
        }

        private void OnBallCollided()
        {
            _animatorController.PlayDiedAnimation();
        }
    }
}