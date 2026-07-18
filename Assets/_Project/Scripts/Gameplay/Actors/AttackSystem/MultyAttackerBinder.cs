using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class MultyAttackerBinder : IActorComponentBinder
    {
        private readonly List<IAttacker> _attackers;
        private readonly AttackerInitializer _attackerInitializer;

        public MultyAttackerBinder(AttackerInitializer attackerInitializer)
        {
            _attackerInitializer = attackerInitializer ?? throw new ArgumentNullException(nameof(attackerInitializer));
            _attackers = new List<IAttacker>();
        }

        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IMultyAttacker multyAttacker) == false)
                return;

            if (view.TryGetViewComponent(out IMultyShotPoint multyShotPoint) == false)
                throw new InvalidOperationException($"'{nameof(view)}' don't contain module '{nameof(IMultyShotPoint)}'");

            _attackers.Clear();
            _attackers.AddRange(multyAttacker.AdditionalAttackers);

            foreach (var attacker in _attackers)
            {
                if (multyShotPoint.TryGetNextShotPoint(out IShotPoint shotPoint) == false)
                    throw new InvalidOperationException($"Number {nameof(shotPoint)}'s cannot be less than {nameof(_attackers.Count)}");

                _attackerInitializer.Init(attacker, shotPoint, view);
            }

            if (view.TryGetViewComponent(out IMultyAttackerViewComponent multyAttackerView))
            {
                int index = 0;

                foreach (var attackerViewComponent in multyAttackerView.AdditionalAttackerView)
                {
                    if (index >= _attackers.Count)
                        break;

                    attackerViewComponent.Init(_attackers[index++]);
                }
            }
        }
    }
}