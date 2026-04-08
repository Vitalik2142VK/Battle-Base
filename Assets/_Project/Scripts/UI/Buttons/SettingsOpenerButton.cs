using UnityEngine;

namespace BattleBase.UI.Buttons
{
    public class SettingsOpenerButton : ButtonClickHandler
    {
        [SerializeField] private PopUps.PopUp _popUp;

        protected override void OnClick() =>
            _popUp.Show();
    }
}