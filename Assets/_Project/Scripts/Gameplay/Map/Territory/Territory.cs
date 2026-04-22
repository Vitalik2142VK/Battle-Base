using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class Territory : MonoBehaviour
    {
        [SerializeField] private List<Territory> _adjacents;
        [SerializeField] private TerritoryTarget _target;

        public event Action ColorChanged;
        public event Action OwnerChanged;

        public TerritoryOwnerType Owner {  get; private set; }

        public Color? Color {  get; private set; }

        public IReadOnlyList<Territory> Adjacents => _adjacents;

        public Transform Target => _target.transform;

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

#if UNITY_EDITOR
        public void AddAdjacent(Territory territory)
        {
            if (territory == null || territory == this) 
                return;

            if (_adjacents.Contains(territory) == false)
            {
                _adjacents.Add(territory);
                territory.AddAdjacentInternal(this);
            }
        }

        public void RemoveAdjacent(Territory territory)
        {
            if (_adjacents.Remove(territory))
                territory.RemoveAdjacentInternal(this);
        }

        private void AddAdjacentInternal(Territory territory)
        {
            if (_adjacents.Contains(territory) == false)
                _adjacents.Add(territory);
        }

        private void RemoveAdjacentInternal(Territory territory) =>
            _adjacents.Remove(territory);
#endif
    }
}