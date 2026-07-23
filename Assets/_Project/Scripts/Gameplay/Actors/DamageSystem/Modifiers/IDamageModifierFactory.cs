using BattleBase.Gameplay.Actors.HealthSystem;

namespace BattleBase.Gameplay.Actors.DamageSystem.Modifiers
{
    public interface IDamageModifierFactory
    {
        public IDamageModifier Create(IHealthComponentSource componentSource);
    }
}