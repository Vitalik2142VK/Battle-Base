using BattleBase.Gameplay.Actors.HealthSystem;

namespace BattleBase.Gameplay.Actors
{
    public interface IHealthComponentSource : IComponentSource
    {
        public IHealthConfig Config { get; }
    }
}
