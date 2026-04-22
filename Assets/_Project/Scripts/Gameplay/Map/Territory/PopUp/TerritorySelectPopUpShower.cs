using System;
using BattleBase.Core;

namespace BattleBase.Gameplay.Map
{
    public class TerritorySelectPopUpShower : ITerritorySelectPopUpShower, IDisposable
    {
        private readonly ITerritorySelector _selector;
        private readonly IPool<TerritorySelectPopUp> _pool;

        private TerritorySelectPopUp _currentPopUp;

        public TerritorySelectPopUpShower(ITerritorySelector selector, IPool<TerritorySelectPopUp> pool)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));

            _selector.Selected += OnTerritorySelected;
            _selector.Unselected += OnTerritoryUnselected;
        }

        public void Dispose()
        {
            _selector.Selected -= OnTerritorySelected;
            _selector.Unselected -= OnTerritoryUnselected;
        }

        private void OnTerritorySelected(Territory territory)
        {
            if (_pool.TryGive(out TerritorySelectPopUp popUp))
            {
                _currentPopUp = popUp;
                _currentPopUp.SetTarget(territory.Target);
                _currentPopUp.Show();

                if (territory.Owner == TerritoryOwnerType.Enemy)
                    _currentPopUp.HideBattleButton();
            }
        }

        private void OnTerritoryUnselected(Territory territory)
        {
            if (_currentPopUp != null)
            {
                _currentPopUp.Hide();
                _currentPopUp = null;
            }
        }
    }
}