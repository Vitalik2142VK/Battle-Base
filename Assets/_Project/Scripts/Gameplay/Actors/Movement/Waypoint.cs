using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class Waypoint : MonoBehaviour, IWaypoint
    {
        private void Awake()
        {
            Position = transform.position;
        }

        public Vector3 Position {  get; private set; }
    }
}