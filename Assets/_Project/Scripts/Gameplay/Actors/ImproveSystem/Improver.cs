using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class Improver : IImprover
    {
        private readonly List<int> _improvePrices;
        private readonly ModifiedImproverData _data;

        private int _currentPriceIndex;

        public Improver(IImproverData data)
        {
            if (data == null)
                throw new System.ArgumentNullException(nameof(data));

            _improvePrices = new List<int>(data.ImprovePrices);
            _data = new ModifiedImproverData(data);
            _currentPriceIndex = 0;
        }

        public IProductionData Data => _data;

        public bool CanImprove => _currentPriceIndex < _improvePrices.Count;

        public void Enable()
        {
            _currentPriceIndex = 0;

            int price = _improvePrices[_currentPriceIndex];
            _data.SetPrice(price);
        }

        public void Improve()
        {
            _currentPriceIndex++;

            if (CanImprove == false)
                return;

            int price = _improvePrices[_currentPriceIndex];
            _data.SetPrice(price);
        }

        public void Disable() { }
    }
}