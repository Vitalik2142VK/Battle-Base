using System;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public class Trackable : MonoBehaviour, ITrackable
    {
        public event Action<ITrackable> Deactivated;
        public event Action<ITrackable> ColorChanged;

        public Transform Transform => transform;

        public Color Color { get; private set; }

        public void SetColor(Color color)
        {
            Color = color;
            ColorChanged?.Invoke(this);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke(this);
        }
    }
}