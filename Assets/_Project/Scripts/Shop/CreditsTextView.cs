using System;
using BattleBase.DI;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleBase.Shop
{
    [RequireComponent(typeof(TMP_Text))]
    public class CreditsTextView : MonoBehaviour, IInjectable
    {
        private const string AvailableCreditsText = "Доступно кредитов: {0}";

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

        private void OnChanged() =>
            _text.text = string.Format(AvailableCreditsText, _credits.Value);
    }
}