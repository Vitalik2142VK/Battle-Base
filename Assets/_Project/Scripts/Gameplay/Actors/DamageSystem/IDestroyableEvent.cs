using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDestroyableEvent
    {
        public event Action Destroyed;
    }
}