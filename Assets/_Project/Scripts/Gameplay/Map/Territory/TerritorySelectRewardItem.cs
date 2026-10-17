using BattleBase.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.Gameplay.Map
{
    public class TerritorySelectRewardItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private LocalizedText _name;

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);

        public void SetInfo(Sprite icon, ILanguageTextsSet nameTextSet)
        {
            _icon.sprite = icon;
            _name.SetTexts(nameTextSet);
        }
    }
}