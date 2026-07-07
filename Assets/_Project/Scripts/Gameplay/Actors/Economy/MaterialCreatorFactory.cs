using System;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialCreatorFactory : IComponentFactory
    {
        private readonly IAdvancedMaterialRegistry _materialRegistry;

        public MaterialCreatorFactory(IAdvancedMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public Type SourceType => typeof(MaterialCreatorSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IMaterialCreatorSource materialCreatorSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IMaterialCreatorSource)}");

            return new MaterialCreator(materialCreatorSource.Config, _materialRegistry);
        }
    }
}