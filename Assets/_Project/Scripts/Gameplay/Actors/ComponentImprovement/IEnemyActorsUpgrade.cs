using BattleBase.ShopSystem;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IEnemyActorsUpgrade
    {
        public IReadOnlyList<IActorItemConfig> Infos { get; }
    }
}