using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerBinder : IActorComponentBinder
    {
        private readonly List<IAttacker> _attackers; 
        private readonly IProjectileSpawner _projectileSpawner;

        private IActor _actor;
        private IActorView _view;

        public AttackerBinder(IProjectileSpawner projectileSpawner)
        {
            _projectileSpawner = projectileSpawner ?? throw new ArgumentNullException(nameof(projectileSpawner));
            _attackers = new List<IAttacker>();
        }

        public void Bind(IActor actor, IActorView view)
        {
            _actor = actor ?? throw new ArgumentNullException(nameof(actor));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            if (_actor.TryGetComponent(out IAttacker attacker) == false)
                return;

            if (_actor.TryGetComponent(out IMultyAttacker multyAttacker))
                BindMultyAttacker(multyAttacker);
            else
                BildAttacker(attacker);
        }

        private void BindMultyAttacker(IMultyAttacker multyAttacker)
        {
            if (_view.TryGetViewComponent(out IMultyShotPoint multyShotPoint) == false)
                throw new InvalidOperationException($"'{nameof(_view)}' don't contain module '{nameof(IMultyShotPoint)}'");

            _attackers.Clear();
            _attackers.AddRange(multyAttacker.Attackers);

            foreach (var attacker in _attackers)
            {
                if (multyShotPoint.TryGetNextShotPoint(out IShotPoint shotPoint) == false)
                    throw new InvalidOperationException($"Number {nameof(shotPoint)}'s cannot be less than {nameof(_attackers.Count)}");

                InitAttacker(attacker, shotPoint);
            }
        }

        private void BildAttacker(IAttacker attacker)
        {
            if (_view.TryGetViewComponent(out IShotPoint shotPoint) == false)
                throw new InvalidOperationException($"'{nameof(_view)}' don't contain module '{nameof(IShotPoint)}'");

            InitAttacker(attacker, shotPoint);

            if (_view.TryGetViewComponent(out IAttackerViewComponent weaponView))
                weaponView.Init(attacker);

            AttackerPresenter presenter = new(attacker);

            if (_view.TryGetViewComponent(out IAim aim))
                aim.Init(presenter, attacker);

            if (_view.TryGetViewComponent(out ITargetFinder targetFinder))
                targetFinder.Init(presenter, attacker.WeaponConfig, _actor);
        }

        private void InitAttacker(IAttacker attacker, IShotPoint shotPoint)
        {
            IWeaponConfig weaponConfig = attacker.WeaponConfig;
            ITargetingProfile targetingProfile = weaponConfig.DamageConfig.TargetingProfile;
            IProjectileConfig projectileConfig = weaponConfig.ProjectileConfig;
            TargetController targetController = new(_view, attacker.WeaponConfig, targetingProfile);
            ProjectileController projectileController = new(_projectileSpawner, shotPoint, projectileConfig);

            attacker.Init(targetController, projectileController);
        }
    }
}