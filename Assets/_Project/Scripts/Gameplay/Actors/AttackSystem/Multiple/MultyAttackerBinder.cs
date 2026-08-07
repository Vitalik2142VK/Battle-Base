using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
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
                    throw new InvalidOperationException($"Number {nameof(shotPoint)}'s cannot be less than {_attackers.Count}");

                _attackerInitializer.Init(attacker, shotPoint, actor.Position);
            }

            if (view.TryGetViewComponent(out IMultyAttackerViewComponent multyAttackerView))
                InitMultyAttackerView(multyAttackerView);

            if (view.TryGetViewComponent(out IMultyAim multyAim))
                InitMultyAim(multyAim);
        }

        private void InitMultyAttackerView(IMultyAttackerViewComponent multyAttackerView)
        {
            int index = 0;

            foreach (var attackerViewComponent in multyAttackerView.AdditionalAttackerView)
            {
                if (index >= _attackers.Count)
                    break;

                attackerViewComponent.Init(_attackers[index++]);
            }
        }

        private void InitMultyAim(IMultyAim multyAim)
        {
            int index = 0;

            IAttacker attacker;
            AttackerPresenter presenter;

            foreach (var aim in multyAim.AdditionalAims)
            {
                if (index >= _attackers.Count)
                    break;

                attacker = _attackers[index++];
                presenter = new AttackerPresenter(attacker);

                aim.Init(presenter, attacker);
            }
        }
    }
}