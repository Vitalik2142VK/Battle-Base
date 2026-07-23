using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Production
{
    [RequireComponent(typeof(Collider))]
    public class ProductionView : MonoBehaviour, IProductionView
    {
        private IProductionPresenter _presenter;
        private ITeamable _teamable;

        public IEnumerable<IProductionOption> ProductionOptions => _presenter.ProductionOptions;

        public TeamType TeamType => _teamable.TeamType;

        public void Init(IProductionPresenter presenter, ITeamable teamable)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }
    }
}