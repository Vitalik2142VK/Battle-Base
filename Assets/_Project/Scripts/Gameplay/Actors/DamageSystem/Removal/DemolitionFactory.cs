using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class DemolitionFactory : IComponentFactory
    {
        public Type SourceType => typeof(DemolitionSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IDemolitionSource demolitionSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IDemolitionSource)}");

            return new Demolition(demolitionSource.Data);
        }
    }
}