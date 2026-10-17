using BattleBase.UI;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IPlayerProductionContext
    {
        public event Action ProductionsChanged;

        public IEnumerable<IProductionItem> GetAvailableItems();

        public void HandleProductionView(IProductionView productionView);

        public void Clear();
    }
}