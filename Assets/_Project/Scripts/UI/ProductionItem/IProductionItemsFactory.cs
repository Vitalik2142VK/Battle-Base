using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.UI
{
    public interface IProductionItemsFactory
    {
        public List<IProductionItem> Create(IEnumerable<IProductionOption> productionOptions);
    }
}