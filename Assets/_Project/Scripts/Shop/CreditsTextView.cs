using System;
using BattleBase.DI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.ShopSystem
{
    [RequireComponent(typeof(TMP_Text))]
    public class CreditsTextView : MonoBehaviour, IInjectable
    {
        [SerializeField] private RectTransform _toRebuildLayout;

        private TMP_Text _text;
        private CreditsModel _credits;

        [Inject]
        public void Construct(CreditsModel credits) =>
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));

        private void Awake() =>
            _text = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _credits.Changed += OnChanged;
            OnChanged();
        }

        private void OnDisable() =>
            _credits.Changed -= OnChanged;

        private void OnChanged()
        {
            _text.text = _credits.Value.ToString();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_toRebuildLayout);
        }
    }
}