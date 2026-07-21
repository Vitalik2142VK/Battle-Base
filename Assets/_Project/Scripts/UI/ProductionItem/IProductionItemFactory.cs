using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.UI
{
    public interface IProductionItemFactory
    {
        public List<IProductionItem> Create(IEnumerable<IProductionOption> productionOptions);
    }
}