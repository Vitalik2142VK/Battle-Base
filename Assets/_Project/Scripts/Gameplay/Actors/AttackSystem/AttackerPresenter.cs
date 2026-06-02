using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerPresenter : IAttackerPresenter
    {
        private readonly IAttacker _model;

        public AttackerPresenter(IAttacker model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public void SpecifyTarget(ITarget target) => 
            _model.SetTarget(target);

        public void EstablishAimState(bool isAimed) =>
            _model.SetAim(isAimed);
    }
}