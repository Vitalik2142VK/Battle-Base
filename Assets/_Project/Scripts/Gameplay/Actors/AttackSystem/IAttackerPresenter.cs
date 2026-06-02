using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackerPresenter
    {
        void SpecifyTarget(ITarget target);

        void EstablishAimState(bool isAimed);
    }
}