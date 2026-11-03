using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorsControllerEvents
    {
        public event Action<IEnumerable<IActor>> DisabledActors;
    }
}
