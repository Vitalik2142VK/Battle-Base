using System;
using BattleBase.UI.PopUp;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.Services.SceneLoadingService
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private PopUpAlphaAnimator _alphaAnimator;

        private bool _isShowed;

        public void Init() =>
            _alphaAnimator.Init();

        public void Show(Action showedCallBack)
        {
            if (_isShowed)
                return;

            _isShowed = true;

            gameObject.SetActive(true);

            if (_alphaAnimator.TryPlayShow(out Tweener tweener))
            {
                tweener.OnComplete(() =>
                {
                    showedCallBack?.Invoke();
                });
            }
        }

        public void Hide(Action hidedCallBack)
        {
            if (_isShowed == false)
                return;

            _isShowed = false;

            if (_alphaAnimator.TryPlayHide(out Tweener tweener))
            {
                tweener.OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    hidedCallBack?.Invoke();
                });
            }
        }
    }
}