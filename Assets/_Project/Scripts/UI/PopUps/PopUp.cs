using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.UI.PopUp;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public class PopUp : MonoBehaviour
    {
        private List<PopUpAnimatorBase> _popUpAnimators;

        public bool IsActive { get; private set; }

        public void Init()
        {
            _popUpAnimators = GetComponents<PopUpAnimatorBase>().ToList();

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
                animator.Init();

            IsActive = gameObject.activeSelf;
        }

        public void Show(Action showedCallBack = null)
        {
            if (IsActive)
                return;

            IsActive = true;
            gameObject.SetActive(true);
            List<Tweener> tweeners = new();

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
            {
                animator.TryPlayShow(out Tweener tweener);
                tweeners.Add(tweener);
            }

            PlaySequence(tweeners, showedCallBack);
        }

        public void Hide(Action hidedCallBack = null)
        {
            if (IsActive == false)
                return;

            IsActive = false;
            List<Tweener> tweeners = new();

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
            {
                animator.TryPlayHide(out Tweener tweener);
                tweeners.Add(tweener);
            }

            PlaySequence(tweeners, () =>
            {
                gameObject.SetActive(false);
                hidedCallBack?.Invoke();
            });
        }

        public void FastHide()
        {
            gameObject.SetActive(false);
            IsActive = false;
        }

        private void PlaySequence(IEnumerable<Tweener> tweeners, Action onComplete) =>
            Static.TweenExtensions.PlaySequence(tweeners, onComplete);
    }
}