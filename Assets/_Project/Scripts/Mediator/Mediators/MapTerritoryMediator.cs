using System;
using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using BattleBase.Gameplay.Map.InputSystem;
using BattleBase.SaveService;
using UnityEngine;
using VContainer;

namespace BattleBase.Mediators
{
    public class MapTerritoryMediator : MediatorBase, ISaveable, IInjectable
    {
        [SerializeField] private TerritorySelector _territorySelector;
        [SerializeField] private List<Territory> _territories;

        private ITerritorySaver _saver;
        private IClickDetector _clickDetector;

        public event Action Changed;

        public IReadOnlyList<Territory> Territories => _territories;

        [Inject]
        public void Construct(
            ITerritorySaver saver,
            IClickDetector clickDetector)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _clickDetector = clickDetector ?? throw new ArgumentNullException(nameof(clickDetector));
        }

        private void OnEnable() =>
            _clickDetector.Clicked += OnClick;

        private void OnDisable() =>
            _clickDetector.Clicked -= OnClick;

        public override void Init()
        {
            if (_territories == null)
                throw new NullReferenceException(nameof(_territories));
        }

        public void Load()
        {
            HashSet<int> conqueredSet = new(_saver.ConqueredTerritories); 

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

            Changed?.Invoke();
        }

        public void Save()
        {
            List<int> conqueredTerritories = new();

            for (int i = 0; i < _territories.Count; i++)
            {
                if (_territories[i].Owner == TerritoryOwnerType.Player)
                    conqueredTerritories.Add(i);
            }

            _saver.SetConqueredTerritories(conqueredTerritories);
        }

        private void OnClick(Collider collider)
        {
            if (collider.TryGetComponent(out Territory territory))
                _territorySelector.Select(territory);
            else
                _territorySelector.Unselect();
        }
    }
}