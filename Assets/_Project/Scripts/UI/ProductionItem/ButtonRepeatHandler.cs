using BattleBase.UI.Buttons;
using BattleBase.Utils;
using System;
using UnityEngine;

namespace BattleBase.UI
{
    public class ButtonRepeatHandler : MonoBehaviour
    {
        [SerializeField] private ButtonHoldHandler _buttonHoldHandler;
        [SerializeField, Min(0.01f)] private float _startRepeatInterval = 0.5f;
        [SerializeField, Min(0.001f)] private float _finishRepeatInterval = 0.2f;
        [SerializeField, Range(0.001f, 0.1f)] private float _repeatDelta = 0.02f;

        private Timer _timer;
        private float _currentRepeat;
        private bool _isDeactivated;

        public event Action Repeated;

        private void Awake()
        {
            _timer = new Timer(_startRepeatInterval);
            _isDeactivated = true;
        }

        private void OnEnable()
        {
            _buttonHoldHandler.HoldActivated += OnActive;
            _buttonHoldHandler.HoldEnded += OnDeactive;
            _currentRepeat = _startRepeatInterval;
        }

        private void Update()
        {
            if (_isDeactivated)
                return;

            _timer.Tick(Time.deltaTime);

            if (_timer.IsTimeUp)
            {
                if (_currentRepeat > _finishRepeatInterval)
                {
                    _currentRepeat -= _repeatDelta;
                    _currentRepeat = Mathf.Clamp(_currentRepeat, _finishRepeatInterval, _startRepeatInterval);
                    _timer.SetWaitTime(_currentRepeat);
                }

                _timer.RestartTimer();

                Repeated?.Invoke();
            }
        }

        private void OnDisable()
        {
            _buttonHoldHandler.HoldActivated -= OnActive;
            _buttonHoldHandler.HoldEnded -= OnDeactive;
        }

        private void OnActive()
        {
            _isDeactivated = false;
            _timer.RestartTimer();

            Repeated?.Invoke();
        }

        private void OnDeactive()
        {
            _currentRepeat = _startRepeatInterval;
            _isDeactivated = true;
        }
    }
}