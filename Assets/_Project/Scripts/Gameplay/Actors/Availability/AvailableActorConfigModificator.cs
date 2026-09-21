using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class AvailableActorConfigModificator : IActorConfig
    {
        private readonly AvailableActorDataModificator _data;
        private readonly IEnumerable<IComponentSource> _components;

        public AvailableActorConfigModificator(IActorConfig config)
        {
            _data = new(config.Data);
            _components = config.GetComponentSources();
        }

        public IActorData Data => _data;

        public IEnumerable<IComponentSource> GetComponentSources() =>
            _components;

        public void SetAvailable(bool isAvailable) =>
            _data.SetAvailable(isAvailable);
    }
}