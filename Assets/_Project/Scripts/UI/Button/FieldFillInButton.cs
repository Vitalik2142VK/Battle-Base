using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.UI.Buttons
{
    public class FieldFillInButton : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private ButtonHoldHandler _buttonHoldHandler;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            _buttonHoldHandler.HoldStarted += OnBeginFillIn;
            _buttonHoldHandler.HoldEnded += OnStopFillIn;
            _fill.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _buttonHoldHandler.HoldStarted -= OnBeginFillIn;
            _buttonHoldHandler.HoldEnded -= OnStopFillIn;
        }

        private void OnBeginFillIn()
        {
            _coroutine = StartCoroutine(BeginFillIn());
        }

        private void OnStopFillIn()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _fill.gameObject.SetActive(false);
        }

        private IEnumerator BeginFillIn()
        {
            _fill.gameObject.SetActive(true);
            float time = 0;

            while (time < _buttonHoldHandler.HoldTime)
            {
                time += Time.deltaTime;
                _fill.fillAmount = 1 - Mathf.Clamp01(time / _buttonHoldHandler.HoldTime);

                yield return null;
            }

            _coroutine = null;
        }
    }
}