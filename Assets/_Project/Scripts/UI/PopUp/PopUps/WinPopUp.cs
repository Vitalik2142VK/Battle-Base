using System;
using System.Collections;
using BattleBase.AdvService;
using BattleBase.UI.Buttons;
using BattleBase.Utils.Constants;
using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleBase.UI.PopUps
{
    public class WinPopUp : PopUp
    {
        [SerializeField] private TMP_Text _credits;
        [SerializeField] private ButtonClickHandler _rewardDoublingButton;
        [SerializeField] private ButtonClickHandler[] _buttons;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _delayBeforeAdditional;
        [SerializeField] private float _delayBeforeDoublingButton = 0.5f;
        [SerializeField] private float _delayBeforeOtherButtons = 0.5f;

        private IAdvertisingService _advertisingService;
        private Coroutine _animationCoroutine;
        private Tween _creditsTween;
        private int _value;

        public event Action<int> RewardIncreased;

        [Inject]
        public void Construct(IAdvertisingService advertisingService) =>
            _advertisingService = advertisingService ?? throw new ArgumentNullException(nameof(advertisingService));

        private void OnEnable()
        {
            _rewardDoublingButton.Clicked += OnRewardDoublingButtonClicked;
            _advertisingService.RewardAdvShown += OnRewardAdvShown;
            _advertisingService.RewardAdvClosed += OnRewardAdvClosed;
        }

        private void OnDisable()
        {
            _rewardDoublingButton.Clicked -= OnRewardDoublingButtonClicked;
            _advertisingService.RewardAdvShown -= OnRewardAdvShown;
            _advertisingService.RewardAdvClosed -= OnRewardAdvClosed;

            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }

            _creditsTween?.Kill();
            _creditsTween = null;
        }

        public void ShowCredits(int basic, int additional)
        {
            _value = basic + additional;
            _credits.text = basic.ToString();

            HideAllButtons();

            if (_animationCoroutine != null)
                StopCoroutine(_animationCoroutine);

            _creditsTween?.Kill();
            _animationCoroutine = StartCoroutine(AnimateCredits(basic, additional));
        }

        private void HideAllButtons()
        {
            if (_rewardDoublingButton != null)
                _rewardDoublingButton.Hide();

            if (_buttons == null)
                return;

            foreach (ButtonClickHandler button in _buttons)
            {
                if (button != null)
                    button.Hide();
            }
        }

        private IEnumerator AnimateCredits(int basic, int additional)
        {
            yield return new WaitForSecondsRealtime(_delayBeforeAdditional);

            int target = basic + additional;

            _creditsTween = DOVirtual.Int(basic, target, _animationDuration, value =>
            {
                if (_credits != null)
                    _credits.text = value.ToString();
            }).SetEase(Ease.OutQuad);

            yield return _creditsTween.WaitForCompletion();
            _creditsTween = null;

            yield return new WaitForSecondsRealtime(_delayBeforeDoublingButton);

            if (_rewardDoublingButton != null)
                _rewardDoublingButton.Show();

            yield return new WaitForSecondsRealtime(_delayBeforeOtherButtons);

            if (_buttons != null)
            {
                foreach (var button in _buttons)
                {
                    if (button != null)
                        button.Show();
                }
            }

            _animationCoroutine = null;
        }

        private void OnRewardDoublingButtonClicked()
        {
            _rewardDoublingButton.Hide();
            _advertisingService.ShowRewardedAdv(AdvertisingRewardNames.RewardDoubling);
        }

        private void OnRewardAdvShown(string id)
        {
            if (id == AdvertisingRewardNames.RewardDoubling)
            {
                RewardIncreased?.Invoke(_value);

                _creditsTween = DOVirtual.Int(_value, _value * 2, _animationDuration, value =>
                {
                    if (_credits != null)
                        _credits.text = value.ToString();
                }).SetEase(Ease.OutQuad);
            }
        }

        private void OnRewardAdvClosed() =>
            _rewardDoublingButton.Show();
    }
}