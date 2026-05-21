using BattleBase.Gameplay.Actors;
using System.Collections.Generic;

namespace BattleBase.UI
{
    public interface IProductionItemFactory
    {
        public List<IProductionItem> Create(IEnumerable<IActorData> infos);
    }
}