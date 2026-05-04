using BattleBase.Core;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public abstract class ActorComponentSource : ScriptableObject, IComponentSource
    {
        public abstract IActorComponent Create();
    }
}
