using Trell.Flexus_TZ.Visual;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
    [AddComponentMenu("Ball (Spike Collision Handler)")]
	public class SpikeCollisionHandler : MonoBehaviour
	{
		[SerializeField] private CollisionEvents _collisionEvents;
        [SerializeField] private DraggingPanelHandler _draggingPanelHandler;
        [SerializeField] private Movement _movement;
        [SerializeField] private DustSpawner _dustSpawner;
        [SerializeField] private Bouncer _spikeCollidedBouncer;

        private void OnEnable()
        {
            _collisionEvents.SpikeCollided += OnSpikeCollided;
        }

        private void OnDisable()
        {
            _collisionEvents.SpikeCollided -= OnSpikeCollided;
        }

        private void OnSpikeCollided(ContactPoint contactPoint)
        {
            _draggingPanelHandler.enabled = false;
            _spikeCollidedBouncer.PlayBounchingAnimation(Vector3.one, Color.white, 1, transform.rotation);
            _movement.VelocityChange(Vector3.zero);
            _dustSpawner.Spawn(contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        }
    }
}