using System;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        public event Action<IEntity> Deactivated;
        public event Action<IEntity> ColorChanged;

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