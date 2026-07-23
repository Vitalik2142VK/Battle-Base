using BattleBase.Gameplay.Actors.Economy;
using System;
using VContainer;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverFactory : IComponentFactory
    {
        private readonly IObjectResolver _resolver;

        private IMaterialRegistry _materialRegistry;

        public ImproverFactory(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public Type SourceType => typeof(ImproverSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IImproverSource improvementSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IImproverSource)}");

            _materialRegistry ??= _resolver.Resolve<IMaterialRegistry>();

            return new ImproverComponent(improvementSource.Data, _materialRegistry);
        }
    }
}