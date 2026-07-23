using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAimComponent
    {
        public bool IsAimed { get; }

        public void LookAtTarget(Vector3 targetPosition, float delta);
    }
}