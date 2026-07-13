using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ShotPoint : MonoBehaviour, IShotPoint
    {
        private void Awake()
        {
            ShotPointTransform = new ShotPointTransform(transform);
        }

        public IShotPointTransform ShotPointTransform { get; private set; }
    }
}