using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerCleanerConnector : IActorComponentConnector
    {
        private IAdvancedPowerRegistry _powerRegistry;

        public PowerCleanerConnector(IAdvancedPowerRegistry powerRegistry)
        {
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
        }

        public void Connect(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actor.Data.Power <= 0)
                return;

            PowerRegistrator cleaner = new(_powerRegistry, actor.Data, actor);
            actor.AddComponent(cleaner);
        }
    }
}
