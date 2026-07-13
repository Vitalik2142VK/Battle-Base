namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IShotPoint : IActorViewComponent
    {
        public IShotPointTransform ShotPointTransform { get; }
    }
}