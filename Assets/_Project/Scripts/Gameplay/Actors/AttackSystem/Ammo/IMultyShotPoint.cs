namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IMultyShotPoint : IActorViewComponent
    {
        public bool TryGetNextShotPoint(out IShotPoint shotPoint);
    }
}