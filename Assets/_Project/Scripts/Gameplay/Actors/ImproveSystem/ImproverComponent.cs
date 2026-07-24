using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production.Improve;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverComponent : IImproverComponent
    {
        private readonly List<int> _improvePrices;
        private readonly IMaterialRegistry _materialRegistry;
        private readonly ImproveProductionData _data;

        private ITeamable _teamable;
        private int _currentPriceIndex;

        public event Action Improved;

        public ImproverComponent(IImproverData data, IMaterialRegistry materialRegistry)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));

            _improvePrices = new List<int>(data.ImprovePrices);
            _data = new ImproveProductionData(data, materialRegistry);
            _currentPriceIndex = 0;
        }

        public Type KeyType => typeof(IImproverComponent);

        public IImproveProductionData Data => _data;

        public int CurrentTier => _currentPriceIndex + 1;

        public bool CanImprove => _currentPriceIndex < _improvePrices.Count;

        public void Init(ITeamable teamable)
        {
            _teamable ??= teamable ?? throw new ArgumentNullException(nameof(teamable));
            _data.Init(_teamable);
        }

        public void Enable()
        {
            _currentPriceIndex = 0;

            int price = _improvePrices[_currentPriceIndex];
            _data.SetPrice(price);
        }

        public bool TryImprove()
        {
            if (CanImprove == false)
                return false;

            int price = _improvePrices[_currentPriceIndex];

            if (_materialRegistry.TrySpend(_teamable.TeamType, price))
            {
                Improved?.Invoke();

                ++_currentPriceIndex;

                if (CanImprove)
                {
                    price = _improvePrices[_currentPriceIndex];
                    _data.SetPrice(price);
                }

                return true;
            }

            return false;
        }

        public void Disable() { }
    }
}