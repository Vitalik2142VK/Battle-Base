using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public interface IHuntMoveData
    {
        public Vector3 Offset { get; }

        public float StoppingDistanceAttack { get; }
    }
}
