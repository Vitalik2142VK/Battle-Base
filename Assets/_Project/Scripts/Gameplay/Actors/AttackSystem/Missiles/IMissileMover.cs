using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Missiles
{
    public interface IMissileMover
    {
        public Vector3 CurrentPosition { get; }

        public bool IsFinished { get; }

        public void Move(float delta);

        public void SetStartPosition(Vector3 startPosition);

        public void SetPointPosition(Vector3 point);
    }
}