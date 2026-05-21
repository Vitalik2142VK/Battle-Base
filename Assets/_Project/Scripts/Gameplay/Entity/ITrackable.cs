using System;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public interface ITrackable
    {
        public event Action<ITrackable> Deactivated;
        public event Action<ITrackable> ColorChanged;

        public Transform Transform { get; }

        public Color Color { get; }

        public void SetColor(Color color);
    }
}