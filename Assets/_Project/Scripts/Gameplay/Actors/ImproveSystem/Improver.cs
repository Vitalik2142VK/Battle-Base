using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class Improver : IImprover
    {
        private readonly ModifiedPriceImproverData _data;

        public Improver(IImproverData data)
        {
            _data = new ModifiedPriceImproverData(data);
        }

        public IImproverData Data => _data;

        public bool CanImprove => true;

        public void Init(IProductionData currentData)
        {
            if (currentData == null)
                throw new System.ArgumentNullException(nameof(currentData));

            _data.SetInitialPrice(currentData.Price);
        }

        public void Disable()
        {
            _data.Reset();
        }

        public void Improve()
        {
            _data.Modify();
        }

        public void Enable() { }
    }
}