using BattleBase.Gameplay.Actors.Movement.Air;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public class HuntMoverFactory : IComponentFactory
    {
        public Type SourceType => typeof(HuntMoveComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IHuntMoveComponentSource huntMoveComponentSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IHuntMoveComponentSource)}");

            IMoveComponentSource moveComponent = huntMoveComponentSource.MoveComponent;
            IMover mover = new Mover(moveComponent.Config);
            IHuntMoveData huntMoveData = huntMoveComponentSource;

            if (moveComponent is IAirMoveComponentSource airMoveComponentSource)
            {
                mover = new AirMover(mover, airMoveComponentSource.Height);

                Vector3 oldOffset = huntMoveData.Offset;
                Vector3 offset = new(oldOffset.x, oldOffset.y + airMoveComponentSource.Height, oldOffset.z);
                huntMoveData = new HuntMoveData(offset, huntMoveData.StoppingDistanceAttack);
            }

            return new HuntMover(mover, huntMoveData);
        }
    }
}
