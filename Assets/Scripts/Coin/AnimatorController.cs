using UnityEngine;

namespace Trell.Flexus_TZ.Coin
{
	[AddComponentMenu("Coin Animator Controller")]
	public class AnimatorController : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private string _dieTriggerName;

		public void PlayDiedAnimation()
        {
			_animator.SetTrigger(_dieTriggerName);
        }
	}
}