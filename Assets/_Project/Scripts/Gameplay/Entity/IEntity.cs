using System;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public interface IEntity
    {
        public event Action<IEntity> Deactivated;
        public event Action<IEntity> ColorChanged;

        public Transform Transform { get; }

        public Color Color { get; }
    }
}