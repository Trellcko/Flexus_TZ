using Trell.Flexus_TZ.Core.Pause;
using Trell.Flexus_TZ.Core.Pool;
using UnityEngine;

namespace Trell.Flexus_TZ.Visual
{
	public class Dust : MonoBehaviour, IPoolable, IPauseHandler
	{
		[SerializeField] private ParticleSystem _spheres;
		[SerializeField] private ParticleSystem _quads;
	
		public float Duration { get; private set; }

        private void Awake()
        {
			Duration = _spheres.main.duration;
			
			float quadsDuration = _quads.main.duration;
			
			if(Duration < quadsDuration)
            {
				Duration = quadsDuration;
            }
        }

        private void OnEnable()
        {
            PauseManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            PauseManager.Instance.UnSubscribe(this);
        }

        public void Play()
        {
			_spheres.Play();
			_quads.Play();
        }

        public void OnPause()
        {
			_spheres.Pause();
			_quads.Pause();
        }

        public void OnUnPause()
        {
			_spheres.Play();
			_quads.Play();
		}
    }
}