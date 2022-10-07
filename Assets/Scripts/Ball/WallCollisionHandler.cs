using Trell.Flexus_TZ.Visual;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [AddComponentMenu("Ball (Wall Collision Handler)")]
    public class WallCollisionHandler : MonoBehaviour
	{
        [SerializeField] private Movement _movement;
        [SerializeField] private CollisionEvents _collisionEvents;
        [SerializeField] private DustSpawner _dustSpawner;
        [SerializeField] private Bouncer _bouncer;
        
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

            Quaternion rotationToNormal = Quaternion.LookRotation(normal);

            _movement.Reflect(normal);

            _bouncer.PlayBounchingAnimation(Vector3.one, Color.white, _movement.Speed, rotationToNormal);
            _dustSpawner.Spawn(contactPoint.point, rotationToNormal);
        }
    }
}