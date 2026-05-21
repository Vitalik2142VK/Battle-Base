namespace BattleBase.Gameplay.MiniMap
{
    public interface IEntityTrackerFactory
    {
        public IEntityTracker CreateTracker(ITrackable entity, PositionTrackingType positionTrackingType);
    }
}