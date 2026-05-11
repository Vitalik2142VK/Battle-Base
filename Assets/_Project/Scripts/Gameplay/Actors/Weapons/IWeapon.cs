using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.Weapons
{
    public interface IWeapon : IActorComponent, IUpdateable, IWeaponEvents
    {
        public void SetTarget(ITarget target);

        public void Shoot();

        public void ShootStop();
    }
}