using System;
using BattleBase.Commands;
using BattleBase.Gameplay.Actors;
using BattleBase.Localization;
using BattleBase.UI.Buttons;
using BattleBase.UI.PopUps;
using BattleBase.Utils.Constants;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.Gameplay.Map
{
    public class TerritorySelectRewardItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private LocalizedText _name;
        [SerializeField] private ButtonClickHandler _infoButton;

        private CommandShowPopUps _commandShowItemInfoPopUp;
        private ItemInfoPopUp _itemInfoPopUp;
        private IActorData _data;

        [Inject]
        public void Construct(
            [Key(VContainerKeys.CommandShowItemInfoPopUp)] CommandShowPopUps commandShowItemInfoPopUp,
            ItemInfoPopUp itemInfoPopUp)
        {
            _commandShowItemInfoPopUp = commandShowItemInfoPopUp != null ? commandShowItemInfoPopUp : throw new ArgumentNullException(nameof(commandShowItemInfoPopUp));
            _itemInfoPopUp = itemInfoPopUp != null ? itemInfoPopUp : throw new ArgumentNullException(nameof(itemInfoPopUp));
        }

        private void OnEnable() =>
            _infoButton.Clicked += OnClickInfo;

        private void OnDisable() =>
            _infoButton.Clicked -= OnClickInfo;

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        public void SetInfo(IActorData actorData)
        {
            _data = actorData ?? throw new ArgumentNullException(nameof(actorData));

            _icon.sprite = actorData.Icon;
            _name.SetTexts(actorData.Name);
            _data = actorData;
        }

        private void OnClickInfo()
        {
            ItemPopUpInfo popUpInfo = new(
                _data.Icon,
                _data.Name,
                _data.Description);

            _itemInfoPopUp.SetInfo(popUpInfo);
            _commandShowItemInfoPopUp.Execute();
        }
    }
}