using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImprovementFactory : IComponentFactory
    {
        public Type SourceType => typeof(ImprovementSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IImprovementSource improvementSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IImprovementSource)}");

            return new Improvement(improvementSource.Data);
        }
    }
}