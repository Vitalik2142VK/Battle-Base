using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public class WinPopUp : PopUp
    {
        [SerializeField] private TMP_Text _credits;
        [SerializeField] private float _delayBeforeAdditional;
        [SerializeField] private float _animationDuration = 0.5f;

        private Coroutine _animationCoroutine;

        public void ShowCredits(int basic, int additional)
        {
            _credits.text = basic.ToString();

            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);

            _animationCoroutine = StartCoroutine(AnimateCredits(basic, additional));
        }

        private IEnumerator AnimateCredits(int basic, int additional)
        {
            yield return new WaitForSecondsRealtime(_delayBeforeAdditional);

            int target = basic + additional;

            DOVirtual.Int(basic, target, _animationDuration, value =>
            {
                if (_credits != null)
                    _credits.text = value.ToString();
            }).SetEase(Ease.OutQuad);

            _animationCoroutine = null;
        }
    }
}