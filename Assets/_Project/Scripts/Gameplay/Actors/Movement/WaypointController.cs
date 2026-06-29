using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class WaypointController : MonoBehaviour, IWaypointController
    {
        [SerializeField] private RouteStartArea[] _routeStartAreas;

        public void SpecifyActorRoute(IMover mover, ISpawnPoint spawnData)
        {
            if (mover == null) 
                throw new ArgumentNullException(nameof(mover));

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