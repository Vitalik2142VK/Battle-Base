using System.Collections.Generic;
using BattleBase.UI.PopUps;
using UnityEngine;

namespace BattleBase.Commands
{
    public class ShowHidePopUpsCommand : CommandBase
    {
        [SerializeField] private List<PopUp> _popUpsToHide;
        [SerializeField] private List<PopUp> _popUpsToShow;

        public override void Execute()
        {
            foreach (PopUp popUp in _popUpsToHide)
                popUp.Hide();

            foreach (PopUp popUp in _popUpsToShow)
                popUp.Show();
        }
    }
}