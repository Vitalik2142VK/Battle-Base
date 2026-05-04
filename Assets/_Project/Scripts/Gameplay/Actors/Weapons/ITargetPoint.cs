using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Weapons
{
    public interface ITargetPoint
    {
        public event Action Destroyed;

        public Vector3 Position { get; }
    }
}