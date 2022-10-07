using System.Collections;
using Trell.Flexus_TZ.Core.Pause;
using UnityEngine;

namespace Trell.Flexus_TZ.Ball
{
	[AddComponentMenu("Ball Animator")]
	public class Bouncer : MonoBehaviour, IPauseHandler
	{
		[SerializeField] private Material _ballMaterial;
		[SerializeField] private Transform _ballTransform;

		[Range(0.1f, 10f)]
		[SerializeField] private float _bouncingAnimationDuration = 0.2F;
		[Range(1, 10)]
		[SerializeField] private float _minPowerToBounce = 1;

		[SerializeField] private AnimationCurve _bouncingCurve;

		[SerializeField] private float _bouncingMultiplayer = 1f;

		[SerializeField] private Vector3 _bouncingScaleMax;

		[SerializeField] private Color _boucningColor;

	    private readonly int _maxPower = 10;
		private readonly int _minPower = 1;


		/// <summary>
		/// Set Power from 1 to 10
		/// </summary>
		/// <param name="power">  </param>
		public void PlayBounchingAnimation(Vector3 startScale, Color startColor, float power, Quaternion rotation)
        {
			power = Mathf.Clamp(power * _bouncingMultiplayer, _minPower, _maxPower);

			if (power < _minPowerToBounce)
			{
				return;
			}

			Vector3 bouncingScale = CalculateBounceScale(power);

			StartCoroutine(PlayBouncingAnimationCorun(startScale, startColor, bouncingScale, _boucningColor, rotation));
        }

		private Vector3 CalculateBounceScale(float power)
        {
			Vector2 xyRange = new Vector2(_bouncingScaleMax.x - 1, _bouncingScaleMax.y - 1);

            float differenceBetweenPowerAndMinPower = (power - _minPower);
			int differenceBetweenMinAndMaxPower = (_maxPower - _minPower); 
			
			Vector2 xy = differenceBetweenPowerAndMinPower * xyRange / (_maxPower - _minPower) + Vector2.one;
			float z = differenceBetweenPowerAndMinPower * (_bouncingScaleMax.z - 1) / differenceBetweenMinAndMaxPower + 1;

			return new Vector3(xy.x, xy.y, z);

        }

		private IEnumerator PlayBouncingAnimationCorun(Vector3 startScale, Color startColor, Vector3 bouncingScale, Color bouncingColor, Quaternion rotation)
        {
			float duration = 0;

			_ballTransform.rotation = rotation;
			while (duration <= _bouncingAnimationDuration)
            {
				if (PauseManager.Instance.IsPaused == false)
				{
					duration = BounceAnimationTick(duration, startScale, bouncingScale, startColor, bouncingColor, rotation);
				}
				yield return null;
            }
			_ballMaterial.color = startColor;
		}

		private float BounceAnimationTick(float duration, Vector3 scaleFrom, Vector3 scaleTo, Color from, Color to, Quaternion rotation)
        {
			duration += Time.deltaTime;
			float percent = Mathf.Clamp01(duration / _bouncingAnimationDuration);

			if(percent <=0.5f)
            {
				transform.rotation = rotation;
			}

			float curvePercent = _bouncingCurve.Evaluate(percent);

			Vector3 scale = Vector3.Lerp(scaleFrom, scaleTo, curvePercent);
			_ballTransform.localScale = scale;

			_ballMaterial.color = Color.Lerp(from, to, curvePercent);
			
			return duration;
		}

        public void OnPause()
        {

        }

        public void OnUnPause()
        {

        }
    }
}