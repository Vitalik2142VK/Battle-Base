using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnerPresenter : IActorSpawnerPresenter
    {
        private readonly IActorSpawner _model;

        public ActorSpawnerPresenter(IActorSpawner model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public IEnumerable<IActorData> ActorsDatas => _model.ActorsData;

        public void SendActorData(IActorData actorData) => 
            _model.SelectActorData(actorData);
    }
}