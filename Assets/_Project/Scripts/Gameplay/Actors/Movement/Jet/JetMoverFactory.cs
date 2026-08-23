using BattleBase.Gameplay.Actors.Movement.Air;
using System;

namespace BattleBase.Gameplay.Actors.Movement.Jet
{
    public class JetMoverFactory : IComponentFactory
    {
        public Type SourceType => typeof(JetMoveComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IJetMoveComponentSource moveSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IMoveComponentSource)}");

            Mover mover = new(moveSource.Config);
            AirMover airMover = new(mover, moveSource.Height);

            return new JetMover(airMover, moveSource.Height, moveSource.OffsetUturn);
        }
    }
}
