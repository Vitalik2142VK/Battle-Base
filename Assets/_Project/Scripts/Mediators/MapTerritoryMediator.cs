using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Abstract;
using BattleBase.Gameplay.Map;
using BattleBase.SaveService;
using UnityEngine;
using VContainer;

namespace BattleBase.Mediators
{
    public class MapTerritoryMediator : MediatorBase, ISaveable, IInjectable
    {
        [SerializeField] private List<Territory> _territories;

        private ITerritorySaver _saver;

        public event Action Changed;

        public IReadOnlyList<Territory> Territories => _territories;

        [Inject]
        public void Construct(ITerritorySaver saver) =>
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

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
                    territory.SetOwner(TerritoryOwnerType.Adjacent);
            }

            Changed?.Invoke();
        }

        public void Save()
        {
            
        }
    }
}