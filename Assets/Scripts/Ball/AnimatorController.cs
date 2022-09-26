using System.Collections;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	[AddComponentMenu("Ball Animator")]
	public class AnimatorController : MonoBehaviour
	{
		[SerializeField] private Material _ballMaterial;
		[SerializeField] private Transform _ballTransform;

		[Range(0.1f, 0.5f)]
		[SerializeField] private float _bouncingAnimationDuration = 0.2F;

		[Range(0.4f, 0.9f)]
		[SerializeField] private float _maxBounchingScale = 0.7F;

		[SerializeField] private Color _boucningColor;

	    private readonly int _maxPower = 10;
		private readonly int _minPower = 1;

		/// <summary>
		/// Set Power from 1 to 10
		/// </summary>
		/// <param name="power">  </param>
		public void PlayBounchingAnimation(float power, Vector3 normal)
        {
			power = Mathf.Clamp(power, _minPower, _maxPower);


			float bouncingScale = 1 - (power * (1 - _maxBounchingScale) / _maxPower);
			print(power +" * "+ _maxBounchingScale +" = " + bouncingScale);
			StartCoroutine(PlayBouncingAnimationCorun(bouncingScale, normal));
        }

		private IEnumerator PlayBouncingAnimationCorun(float bouncingScale, Vector3 normal)
        {
			Quaternion rotattion = Quaternion.LookRotation(normal);
			float halfDuration = _bouncingAnimationDuration / 2f;

			float duration = 0;
			while(duration <= halfDuration)
            {
				duration += Time.deltaTime;

				float percent = Mathf.Clamp01(duration / halfDuration);

				float z = Mathf.Lerp(1, bouncingScale, percent);
				_ballTransform.localScale = new Vector3(1, 1, z);
				_ballMaterial.color = Color.Lerp(Color.white, _boucningColor, percent);
				_ballTransform.rotation = rotattion;
				yield return null;
            }

			duration = 0;

			while (duration <= halfDuration)
			{
				duration += Time.deltaTime;

				float percent = Mathf.Clamp01(duration / halfDuration);

				float z = Mathf.Lerp(bouncingScale, 1, percent);
				_ballTransform.localScale = new Vector3(1, 1, z);

				_ballMaterial.color = Color.Lerp(_boucningColor, Color.white, percent);
				yield return null;
			}

		}
	}
}