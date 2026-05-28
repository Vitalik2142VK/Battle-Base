using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class WaypointController : MonoBehaviour, IWaypointController
    {
        [SerializeField] private RouteStartArea[] _routeStartAreas;

        public void SpecifyActorRoute(IActor actor, ISpawnData spawnData)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IMover mover) == false)
                return;

            if (spawnData == null)
                throw new ArgumentNullException(nameof(spawnData));

            var waipoints = GetWaipointsByPosition(spawnData.SpawnPosition);
            mover.AddWaypoints(waipoints);
        }

        private IEnumerable<IWaypoint> GetWaipointsByPosition(Vector3 position)
        {
            foreach (var routeStartArea in _routeStartAreas)
            {
                if (routeStartArea.HasInArea(position))
                    return routeStartArea.Route;
            }

            throw new InvalidOperationException($"No area contains the position - {position}");
        }
    }
}