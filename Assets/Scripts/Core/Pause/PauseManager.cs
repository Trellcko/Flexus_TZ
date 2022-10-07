using System.Collections.Generic;
using UnityEngine;

namespace Trell.Flexus_TZ.Core.Pause
{
	public class PauseManager : MonoBehaviour
	{
		public static PauseManager Instance 
		{
			get 
			{
				if(s_instance == null)
                {
					s_instance = new GameObject(nameof(PauseManager)).AddComponent<PauseManager>();
					DontDestroyOnLoad(s_instance);
                }
				return s_instance;
			}
		}

		private static PauseManager s_instance;

		private List<IPauseHandler> _subscribers = new List<IPauseHandler>();

		public bool IsPaused { get; private set; } = false;

        private void Awake()
        {
            if(FindObjectsOfType<PauseManager>().Length > 1)
            {
				Destroy(gameObject);
            }
        }

        public void Pause()
        {
			IsPaused = true;
			foreach(var subscriber in _subscribers)
            {
				subscriber.OnPause();
            }
        }

		public void UnPause()
        {
			IsPaused = false;
			foreach(var subscirbe in _subscribers)
            {
				subscirbe.OnUnPause();
            }
        }

		public void Subscribe(IPauseHandler paused)
        {
			_subscribers.Add(paused);
        }
	}
}