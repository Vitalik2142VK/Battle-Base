using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class Territory : MonoBehaviour
    {
        [SerializeField] private List<Territory> _adjacents;

        public event Action OwnerChanged;

        public Transform Target => transform;

        public TerritoryOwnerType Owner { get; private set; }

        public IReadOnlyList<Territory> Adjacents => _adjacents;

        public int Index {  get; private set; }

        public void SetOwner(TerritoryOwnerType owner)
        {
            Owner = owner;
            OwnerChanged?.Invoke();
        }

        public void SetIndex(int index) =>
            Index = index;

#if UNITY_EDITOR
        public void AddAdjacent(Territory territory)
        {
            if (territory == null || territory == this)
                return;

            if (_adjacents.Contains(territory) == false)
            {
                _adjacents.Add(territory);
                territory.AddAdjacent(this);
            }
        }

        public void RemoveAdjacent(Territory territory)
        {
            if (_adjacents.Remove(territory))
                territory.RemoveAdjacent(this);
        }
#endif
    }
}