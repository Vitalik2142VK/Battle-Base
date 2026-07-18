using BattleBase.Gameplay.Actors.AttackSystem.Ammo;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    public interface IMultyShotPoint : IActorViewComponent
    {
        public bool TryGetNextShotPoint(out IShotPoint shotPoint);
    }
}