using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAimComponent
    {
        public bool IsAimed { get; }

        public bool IsRestored { get; }

        public void LookAtTarget(Vector3 targetPosition, float delta);

        public void ReturnToStart(float delta);
    }
}