using System;
using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public interface ITrackable
    {
        public event Action<ITrackable> Deactivated;
        public event Action<ITrackable> ColorChanged;

        public Transform Transform { get; }

        public Color Color { get; }
    }
}