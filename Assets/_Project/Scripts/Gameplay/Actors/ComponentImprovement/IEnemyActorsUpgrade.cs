using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IEnemyActorsUpgrade
    {
        public IEnumerable<EnemyActorUpgradeStats> Stats { get; }
    }
}