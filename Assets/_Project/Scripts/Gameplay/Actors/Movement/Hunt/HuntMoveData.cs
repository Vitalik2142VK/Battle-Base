using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public class HuntMoveData : IHuntMoveData
    {
        public HuntMoveData(Vector3 offset, float stoppingDistanceAttack)
        {
            if (stoppingDistanceAttack < 0)
                throw new System.ArgumentOutOfRangeException(nameof(stoppingDistanceAttack));

            Offset = offset;
            StoppingDistanceAttack = stoppingDistanceAttack;
        }

        public Vector3 Offset { get; }

        public float StoppingDistanceAttack { get; }
    }
}
