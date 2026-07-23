using BattleBase.Gameplay.Actors.DamageSystem;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerPresenter : IAttackerPresenter
    {
        private readonly IAttacker _model;

        public AttackerPresenter(IAttacker model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public void SetTargets(IEnumerable<ITarget> targets) => 
            _model.SetTargets(targets);

        public void EstablishAimState(bool isAimed) =>
            _model.SetAim(isAimed);
    }
}