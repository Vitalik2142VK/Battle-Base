using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDestroyableEvents
    {
        public event Action Destroyed;
    }
}