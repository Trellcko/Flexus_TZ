using Trell.Flexus_TZ.Core.Pause;
using UnityEngine;

namespace Trell.Flexus_TZ.Coin
{
	[AddComponentMenu("Coin Animator Controller")]
	public class AnimatorController : MonoBehaviour, IPauseHandler
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private string _dieTriggerName;

        [SerializeField] private ParticleSystem[] _particleSystems;

        private void OnEnable()
        {
            PauseManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            PauseManager.Instance.UnSubscribe(this);
        }

        public void OnPause()
        {
            _animator.speed = 0;
            foreach(var particleSystem in _particleSystems)
            {
                particleSystem.Pause();
            }
        }

        public void OnUnPause()
        {
            _animator.speed = 1;
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }

        public void PlayDiedAnimation()
        {
			_animator.SetTrigger(_dieTriggerName);
        }
	}
}