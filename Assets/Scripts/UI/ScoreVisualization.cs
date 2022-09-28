using System.Collections;
using TMPro;
using Trell.Flexus_TZ.ScoreSystem;
using UnityEngine;

namespace Trell.Flexus_TZ.UI
{
	public class ScoreVisualization : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _coinText;

		[SerializeField] private Score _score;
        [SerializeField] private AnimationCurve _scalingCurve;

        [Range(0.1f, 10f)]
        [SerializeField] private float _animationTime;

        [Range(0.1f, 1f)]
        [SerializeField] private float _timeForOneScaling;

        [SerializeField] private Vector3 _textScaleMax;

        private Transform _coinTextTransform;

        private float _currentDisplayNumber = 0;

        private float _currentScore;

        private Coroutine _updateTextCorun;

        private void Awake()
        {
            _coinText.SetText(_currentDisplayNumber.ToString());
            _coinTextTransform = _coinText.transform;
        }

        private void OnEnable()
        {
            _score.Changed += UpdateScore;
        }

        private void OnDisable()
        {
            _score.Changed -= UpdateScore;
        }

        private void UpdateScore()
        {
            _currentScore = _score.Value;
            if(_updateTextCorun != null)
            {
                StopCoroutine(_updateTextCorun);
            }
            _updateTextCorun = StartCoroutine(UpdateTextCorun());
         
        }

        private IEnumerator UpdateTextCorun()
        {
            float duration = 0;

            float initNumber = _currentDisplayNumber;
            _coinTextTransform.localScale = Vector3.one;
            
            while (duration <= _animationTime)
            {
                duration = UpdateTextTick(duration, initNumber, _score.Value, Vector3.one, _textScaleMax);
                yield return null;
            }
            _coinTextTransform.localScale = Vector3.one;
        }

        private float UpdateTextTick(float duration, float fromNumber, float toNumber, Vector3 fromScale, Vector3 toScale)
        {
            duration += Time.deltaTime;
            float percentText = Mathf.Clamp01(duration / _animationTime);

            float percentScaling = Mathf.Clamp01((duration % _timeForOneScaling) / _timeForOneScaling);

            float curvePercentScaling = _scalingCurve.Evaluate(percentScaling);

            _coinTextTransform.localScale = Vector3.Lerp(fromScale, toScale, curvePercentScaling);
            
            _currentDisplayNumber = Mathf.Lerp(fromNumber, toNumber, percentText);
            print(percentText);

            _coinText.SetText(_currentDisplayNumber.ToString("N0"));
            return duration;
        }
    }
}