using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Movement
{
    public interface IMover : IActorComponent, IMoverEvents
    {
        public IMoveConfig Config { get; }

        public bool CanMove { get; }

        public void EstablishWaypoints(IEnumerable<IWaypoint> waypoints);

        public void EstablishNextPoint();

        public void Move();

        public void Stop();
    }
}
