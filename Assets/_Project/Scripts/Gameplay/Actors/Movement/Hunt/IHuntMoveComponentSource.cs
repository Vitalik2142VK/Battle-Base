namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public interface IHuntMoveComponentSource : IComponentSource, IHuntMoveData
    {
        public IMoveComponentSource MoveComponent { get; }
    }
}
