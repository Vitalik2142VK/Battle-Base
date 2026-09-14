using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public interface IHealthEvents : IDestroyableEvent
    {
        public event Action<float, float> HealthChanged;
    }
}