using System;
using BattleBase.Core;
using BattleBase.Gameplay.CameraNavigation.InputReader;

namespace BattleBase.Gameplay.Map
{
    public class TerritoryPopUpShower : ITerritoryPopUpShower, IDisposable
    {
        private readonly ITerritorySelector _selector;
        private readonly IPool<TerritorySelectPopUp> _pool;
        private readonly IUIPointerChecker _pointerChecker;

        private TerritorySelectPopUp _currentPopUp;

        public TerritoryPopUpShower(ITerritorySelector selector, IPool<TerritorySelectPopUp> pool, IUIPointerChecker uIPointerChecker)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
            _pointerChecker = uIPointerChecker ?? throw new ArgumentNullException(nameof(uIPointerChecker));

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
                TerritoryOwnerType owner = territory.Owner;

                _currentPopUp = popUp;
                _currentPopUp.SetTarget(territory.Target);
                _currentPopUp.SetInfo(territory.Info);
                _currentPopUp.SetOwner(owner);
                _pointerChecker.AddCanvas(_currentPopUp.Canvas);
                _currentPopUp.Show();
            }
        }

        private void OnTerritoryUnselected(Territory territory)
        {
            if (_currentPopUp != null)
            {
                _currentPopUp.Hide();
                _pointerChecker.RemoveCanvas(_currentPopUp.Canvas);
                _currentPopUp = null;
            }
        }
    }
}