using System;
using TMPro;
using UnityEngine;

namespace BattleBase.Services.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private LanguageTextsSet _texts;

        private TMP_Text _text;

        private void Awake()
        {
            if (TryGetComponent(out _text) == false)
                throw new NullReferenceException($"Object: {name}, NullComponent: {nameof(_text)}");
        }

        private void OnEnable()
        {
            YandexGameLanguageSystemAdapter.Changed += OnSwitchLanguage;
            OnSwitchLanguage();
        }

        private void OnDisable() =>
            YandexGameLanguageSystemAdapter.Changed -= OnSwitchLanguage;

        private void OnSwitchLanguage()
        {
            TextLangParams langParams =
                _texts.GetByLanguage(YandexGameLanguageSystemAdapter.CurrentLanguage)
                ?? throw new ArgumentNullException($"{nameof(LocalizedText)} on {name} has no params for current language");

            _text.text = langParams.Text;
            //_text.font = langParams.Font;
            //_text.fontMaterial = langParams.Preset;
        }
    }
}