using Trell.Flexus_TZ.UI;
using Trell.Flexus_TZ.Visual;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	public class GoalCollisionHandler : MonoBehaviour
	{
		[SerializeField] private CollisionEvents _collisionEvents;
		[SerializeField] private Movement _movement;
		[SerializeField] private DustSpawner _dustSpawner;
		[SerializeField] private Bouncer _bouncer;
        [SerializeField] private DraggingPanelHandler _draggingPanelHandler;
        [SerializeField] private CompleteLevelPanel _completedLevelPanel;

        private void OnEnable()
        {
            _collisionEvents.GoalCollided += OnGoalCollided;
        }

        private void OnDisable()
        {
            _collisionEvents.GoalCollided -= OnGoalCollided;
        }

        private void OnGoalCollided(ContactPoint obj)
        {
            var rotation = Quaternion.LookRotation(obj.normal);
            _dustSpawner.Spawn(obj.point, rotation);
            _bouncer.PlayBounchingAnimation(Vector3.one, Color.white, _movement.Speed, rotation);
            _movement.VelocityChange(Vector3.zero);
            _draggingPanelHandler.enabled = false;
            _completedLevelPanel.gameObject.SetActive(true);
        }
    }
}