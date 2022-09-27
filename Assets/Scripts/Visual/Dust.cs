using Trell.Flexus_TZ.Core.Pool;
using UnityEngine;

namespace Trell.Flexus_TZ.Visual
{
	public class Dust : MonoBehaviour, IPoolable
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

		public void Play()
        {
			_spheres.Play();
			_quads.Play();
        }
    }
}