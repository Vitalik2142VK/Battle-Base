using BattleBase.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.UI.PopUps
{
    public class ItemInfoPopUp : PopUp
    {
        [SerializeField] private LocalizedText _name;
        [SerializeField] private LocalizedText _description;
        [SerializeField] private Image _preview;

        public void SetInfo(ItemPopUpInfo info)
        {
            _preview.sprite = info.Preview;
            _name.SetTexts(info.Name);
            _description.SetTexts(info.Description);
        }
    }
}