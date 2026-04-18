using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IClickDetector
    {
        public event Action<Collider> Clicked;
    }
}