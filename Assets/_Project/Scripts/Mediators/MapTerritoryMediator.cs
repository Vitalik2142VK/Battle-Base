using System;
using System.Collections.Generic;
<<<<<<< HEAD:Assets/_Project/Scripts/Mediator/Mediators/MapTerritoryMediator.cs
using BattleBase.DI;
=======
using System.Linq;
using BattleBase.Abstract;
>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96:Assets/_Project/Scripts/Mediators/MapTerritoryMediator.cs
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
            foreach (Territory territory in _territories)
                territory.Init();
        }

        public void Load()
        {
            IReadOnlyList<int> conqueredTerritories = _saver.ConqueredTerritories;

            for (int i = 0; i < _territories.Count; i++)
            {
                if (conqueredTerritories.Contains(i))
                    _territories[i].SetOwner(TerritoryOwnerType.Player);
                else
                    _territories[i].SetOwner(TerritoryOwnerType.Enemy);
            }

            foreach (Territory territory in _territories)
            {
                if (territory.Owner != TerritoryOwnerType.Player)
<<<<<<< HEAD:Assets/_Project/Scripts/Mediator/Mediators/MapTerritoryMediator.cs
                    continue;

                foreach (Territory adjacent in territory.Adjacents)
                {
                    if (adjacent != null && adjacent.Owner != TerritoryOwnerType.Player)
                        adjacent.SetOwner(TerritoryOwnerType.Contested);
                }
=======
                    territory.SetOwner(TerritoryOwnerType.Adjacent);
>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96:Assets/_Project/Scripts/Mediators/MapTerritoryMediator.cs
            }

            Changed?.Invoke();
        }

        public void Save()
        {
            
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