using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverFactory : IComponentFactory
    {
        public Type SourceType => typeof(ImproverSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IImproverSource improvementSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IImproverSource)}");

            return new Improver(improvementSource.Data);
        }
    }
}