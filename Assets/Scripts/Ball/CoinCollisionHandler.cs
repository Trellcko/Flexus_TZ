using Trell.Flexus_TZ.ScoreSystem;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	[AddComponentMenu("Ball (Coin Collision Handler)")]
	public class CoinCollisionHandler : MonoBehaviour
	{
		[SerializeField] private CollisionEvents _collisionEvents;
		[SerializeField] private Score _score;

        [Min(1)]
        [SerializeField] private int _scoresForCoin;

        private void OnEnable()
        {
            _collisionEvents.CoinCollided += OnCoinCollided;
        }

        private void OnDisable()
        {
            _collisionEvents.CoinCollided -= OnCoinCollided;
        }

        private void OnCoinCollided()
        {
            _score.Add(_scoresForCoin);
        }
    }
}