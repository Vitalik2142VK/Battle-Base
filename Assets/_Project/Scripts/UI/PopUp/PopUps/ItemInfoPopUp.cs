using BattleBase.Gameplay.Actors;
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

        public void SetInfo(IActorData info)
        {
            _name.SetTexts(info.Name);
            _preview.sprite = info.Icon;
            _description.SetTexts(info.Description);
        }
    }
}