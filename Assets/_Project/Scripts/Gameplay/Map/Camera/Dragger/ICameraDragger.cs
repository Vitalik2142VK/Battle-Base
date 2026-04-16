using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraDragger
    {
        public void Update(float deltaTime, Vector3? worldDragDelta);
    }
}