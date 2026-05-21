using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerPresenter
    {
        public IEnumerable<IActorData> ActorsDatas { get; }

        public void SendActorData(IActorData actorData);
    }
}