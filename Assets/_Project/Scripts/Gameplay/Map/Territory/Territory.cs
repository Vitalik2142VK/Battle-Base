using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class Territory : MonoBehaviour
    {
        [SerializeField] private List<Territory> _adjacents;

        public event Action ColorChanged;
        public event Action OwnerChanged;

        public TerritoryOwnerType Owner {  get; private set; }

        public Color? Color {  get; private set; }

        public IReadOnlyList<Territory> Adjacents => _adjacents;

        public void Init()
        {
            
        }

        public void SetColor(Color color)
        {
            Color = color;
            ColorChanged?.Invoke();
        }

        public void SetOwner(TerritoryOwnerType owner)
        {
            Owner = owner;
            OwnerChanged?.Invoke();
        }
    }
}