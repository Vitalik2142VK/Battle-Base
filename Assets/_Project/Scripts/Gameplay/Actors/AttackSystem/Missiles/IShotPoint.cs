using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Missiles
{
    public interface IShotPoint : IActorViewComponent
    {
        public Vector3 Position { get; }
    }
}