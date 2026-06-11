using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IShotPoint : IActorViewComponent
    {
        public Vector3 Position { get; }
    }
}