using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public class PopUp : MonoBehaviour
    {
        private readonly List<Tweener> _currentTweens = new();

        private List<PopUpAnimatorBase> _popUpAnimators;

        public bool IsActive { get; private set; }

        public void Init()
        {
            _popUpAnimators = GetComponents<PopUpAnimatorBase>().ToList();

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
                animator.Init();

            IsActive = gameObject.activeSelf;
        }

        public void Show(Action shownCallback = null)
        {
            if (IsActive)
                return;

            KillCurrentTweens();
            IsActive = true;
            gameObject.SetActive(true);

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
            {
                animator.TryPlayShow(out Tweener tweener);

                if (tweener != null)
                    _currentTweens.Add(tweener);
            }

            PlaySequence(_currentTweens, () =>
            {
                _currentTweens.Clear();
                shownCallback?.Invoke();
            });
        }

        public void Hide(Action hiddenCallBack = null)
        {
            if (IsActive == false)
                return;

            KillCurrentTweens();
            IsActive = false;

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
            {
                animator.TryPlayHide(out Tweener tweener);

                if (tweener != null)
                    _currentTweens.Add(tweener);
            }

            PlaySequence(_currentTweens, () =>
            {
                _currentTweens.Clear();
                gameObject.SetActive(false);
                hiddenCallBack?.Invoke();
            });
        }

        public void HideInstantly()
        {
            KillCurrentTweens();
            gameObject.SetActive(false);
            IsActive = false;
        }

        private void KillCurrentTweens()
        {
            foreach (Tweener tweener in _currentTweens)
            {
                if (tweener != null && tweener.IsActive())
                    tweener.Kill(false);
            }

            _currentTweens.Clear();
        }

        private void PlaySequence(IEnumerable<Tweener> tweeners, Action onComplete) =>
            Static.TweenExtensions.PlaySequence(tweeners, onComplete);
    }
}