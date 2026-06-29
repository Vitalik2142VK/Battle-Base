using System;
using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;

namespace BattleBase.Mediators
{
    public class MapTerritoryMediator : MediatorBase, IInjectable
    {
        [SerializeField] private List<Territory> _territories;

        private IClickDetector _clickDetector;
        private ITerritorySelector _territorySelector;
        private TerritoryStatusIndicatorFactory _territoryStatusIndicatorFactory;
        private TerritoriesModel _territoriesModel;

        public IReadOnlyList<Territory> Territories => _territories;

        [Inject]
        public void Construct(
            IClickDetector clickDetector,
            ITerritorySelector territorySelector,
            TerritoryStatusIndicatorFactory territoryStatusIndicatorFactory,
            TerritoriesModel territoriesModel)
        {
            _clickDetector = clickDetector ?? throw new ArgumentNullException(nameof(clickDetector));
            _territorySelector = territorySelector ?? throw new ArgumentNullException(nameof(territorySelector));
            _territoryStatusIndicatorFactory = territoryStatusIndicatorFactory ?? throw new ArgumentNullException(nameof(territoryStatusIndicatorFactory));
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));
        }

        private void OnEnable()
        {
            _clickDetector.Clicked += OnClick;
            _territoriesModel.Changed += OnTerritoriesModelsChanged;
            OnTerritoriesModelsChanged();
        }

        private void OnDisable()
        {
            _clickDetector.Clicked -= OnClick;
            _territoriesModel.Changed -= OnTerritoriesModelsChanged;
        }

        public override void Init()
        {
            for (int i = 0; i < _territories.Count; i++)
            {
                Territory territory = _territories[i];
                territory.SetIndex(i);
                TerritoryStatusIndicator indicator = _territoryStatusIndicatorFactory.Create();
                indicator.SetTerritory(territory);
            }
        }

        private void OnClick(Collider collider)
        {
            if (collider == null)
            {
                _territorySelector.Unselect();

                return;
            }

            if (collider.TryGetComponent(out Territory territory))
                _territorySelector.Select(territory);
            else
                _territorySelector.Unselect();
        }

        private void OnTerritoriesModelsChanged()
        {
            HashSet<int> conqueredSet = new(_territoriesModel.ConqueredTerritories);

            for (int i = 0; i < _territories.Count; i++)
            {
                TerritoryOwnerType owner = conqueredSet.Contains(i)
                    ? TerritoryOwnerType.Player
                    : TerritoryOwnerType.Enemy;

                _territories[i].SetOwner(owner);
            }

            foreach (Territory territory in _territories)
            {
                if (territory.Owner != TerritoryOwnerType.Player)
                    continue;

                foreach (Territory adjacent in territory.Adjacents)
                {
                    if (adjacent != null && adjacent.Owner != TerritoryOwnerType.Player)
                        adjacent.SetOwner(TerritoryOwnerType.Contested);
                }
            }
        }
    }
}