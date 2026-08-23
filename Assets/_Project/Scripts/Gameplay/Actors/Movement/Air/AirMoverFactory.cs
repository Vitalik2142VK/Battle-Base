using System;

namespace BattleBase.Gameplay.Actors.Movement.Air
{
    public class AirMoverFactory : IComponentFactory
    {
        public Type SourceType => typeof(AirMoveComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IAirMoveComponentSource moveSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IMoveComponentSource)}");

            Mover mover = new(moveSource.Config);

            return new AirMover(mover, moveSource.Height);
        }
    }
}
