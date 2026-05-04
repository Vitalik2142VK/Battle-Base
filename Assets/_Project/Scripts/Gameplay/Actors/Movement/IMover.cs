using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public interface IMover
    {
        public void Move();

        public void SetPointPosition(Vector3 point);
    }
}
