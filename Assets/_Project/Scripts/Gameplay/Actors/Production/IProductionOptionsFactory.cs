using BattleBase.Core;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionOptionsFactory : IFactory<IEnumerable<IProductionOption>> { }
}