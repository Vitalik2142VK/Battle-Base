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
        [SerializeField] private List<PopUp> _popUpsToHide;
        [SerializeField] private PopUpAudio _audio;

        private List<PopUpAnimatorBase> _popUpAnimators;

        public event Action<PopUp> Changed;

        public bool IsActive { get; private set; }

        public void Init()
        {
            _popUpAnimators = GetComponents<PopUpAnimatorBase>().ToList();

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
                animator.Init();

            IsActive = gameObject.activeSelf;
        }

        public void Show()
        {
            if (IsActive)
                return;

            IsActive = true;
            gameObject.SetActive(true);

            foreach (PopUpAnimatorBase animator in _popUpAnimators)
                animator.TryPlayShow(out _);

            if (_audio != null)
                _audio.PlayShowing();

            Changed?.Invoke(this);

            foreach (PopUp popUp in _popUpsToHide)
                popUp.Hide();
        }

        public void Hide()
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

            if (tweeners.Count == 0)
            {
                gameObject.SetActive(false);

                return;
            }

            Sequence sequence = DOTween.Sequence()
                .SetUpdate(true);

            foreach (Tweener tweener in tweeners)
                sequence.Join(tweener);

            if (_audio != null)
                _audio.PlayHidding();

            sequence.OnComplete(() =>
            {
                HideInternal();
            });
        }

        public void FastHide()
        {
            gameObject.SetActive(false);
            IsActive = false;
        }

        private void HideInternal()
        {
            gameObject.SetActive(false);
            Changed?.Invoke(this);
        }
    }
}