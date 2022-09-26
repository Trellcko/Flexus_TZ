using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	public class CollisionHandler : MonoBehaviour
	{
        [SerializeField] private Movement _movement;
        [SerializeField] private CollisionEvents _collisionEvents;
        [SerializeField] private Bouncer _bouncer;
        [SerializeField] private ParticleSystem _dustEffect;

        private void OnEnable()
        {
            _collisionEvents.WallCollided += OnWallCollided;
        }

        private void OnDisable()
        {
            _collisionEvents.WallCollided -= OnWallCollided;
        }

        private void OnWallCollided(ContactPoint contactPoint)
        {
            Vector3 normal = contactPoint.normal;

            _movement.Reflect(normal);

            _bouncer.PlayBounchingAnimation(_movement.Speed, normal);

            _dustEffect.transform.position = contactPoint.point;
            _dustEffect.transform.rotation = Quaternion.LookRotation(normal);
            _dustEffect.Play();
        }
    }
}