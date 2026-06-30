using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class Improvement : IImprovement
    {
        private readonly ModifiedPriceImprovementData _data;

        public Improvement(IImprovementData data)
        {
            _data = new ModifiedPriceImprovementData(data);
        }

        public IImprovementData Data => _data;

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