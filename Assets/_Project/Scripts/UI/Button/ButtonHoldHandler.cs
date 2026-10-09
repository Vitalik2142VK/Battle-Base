using BattleBase.Utils;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleBase.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField, Min(0.01f)] private float _holdTime = 1f;

        private Button _button;
        private Timer _timer;

        private bool _isHolding;
        private bool _isExecuted;

        public event Action HoldStarted;
        public event Action HoldActivated;
        public event Action HoldEnded;

        public float HoldTime => _holdTime;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _timer = new Timer(_holdTime);
        }

        private void OnEnable()
        {
            PointerUp();
        }

        private void Update()
        {
            if (_isHolding == false || _isExecuted)
                return;

            _timer.Tick(Time.deltaTime);

            if (_timer.IsTimeUp)
            {
                _isExecuted = true;
                _isHolding = false;

                HoldActivated?.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData _)
        {
            if (_button.interactable == false)
                return;

            _isHolding = true;
            _isExecuted = false;
            _timer.RestartTimer();

            HoldStarted?.Invoke();
        }

        public void OnPointerUp(PointerEventData _) =>
            PointerUp();

        private void PointerUp()
        {
            _isHolding = false;

            HoldEnded?.Invoke();
        }
    }
}