using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class Demolition : IDemolition
    {
        private readonly IDemolitionData _data;
        private readonly IAdvancedMaterialRegistry _materialRegistry;

        private IPriceCounterDemolition _priceCounter;
        private ITeamable _teamable;

        public event Action Destroyed;

        public Demolition(IDemolitionData data, IAdvancedMaterialRegistry materialRegistry)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));

            Data = _data;
        }

        public IDemolitionData Data { get; private set; }

        public void Init(IPriceCounterDemolition priceCounter, ITeamable teamable)
        {
            _priceCounter = priceCounter ?? throw new ArgumentNullException(nameof(priceCounter));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));

            Data = _priceCounter;
        }

        public void Enable() =>
            _priceCounter.Enable();

        public void Disable() =>
            _priceCounter.Disable();

        public void Demolish()
        {
            _materialRegistry.AddMaterials(_teamable.TeamType, _priceCounter.Price);

            Destroyed?.Invoke();
        }
    }
}