using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionStorage
    {
        public IEnumerable<IProductionOption> GetProductionOptions();
    }
}