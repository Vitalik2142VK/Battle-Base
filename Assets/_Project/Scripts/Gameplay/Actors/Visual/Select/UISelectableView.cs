using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Select
{
    public class UISelectableView : SelectableView
    {
        [SerializeField] private Canvas _ui;

        protected override void HandleInactiveState() =>
            _ui.gameObject.SetActive(false);

        protected override void HandleActiveState() =>
            _ui.gameObject.SetActive(true);

        protected override void HandleSelectedState() =>
            _ui.gameObject.SetActive(true);
    }
}