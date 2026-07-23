using System;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionServiceFactory : IComponentFactory
    {
        public Type SourceType => typeof(ProductionServiceSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is ProductionServiceSource productionServiceSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(ProductionServiceSource)}");


            return new ProductionService();
        }
    }
}