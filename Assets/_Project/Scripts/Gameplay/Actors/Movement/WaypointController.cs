using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class WaypointController : MonoBehaviour, IWaypointController
    {
        [SerializeField] private RouteStartArea[] _routeStartAreas;

        public void SpecifyActorRoute(IActor actor)
        {
            if (actor == null) 
                throw new System.ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IMover mover) == false)
                return;

            mover.AddWaypoints(null); //todo
            mover.EstablishNextPoint();
            mover.Move();
        } 
    }
}