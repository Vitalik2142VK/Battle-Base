using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    [System.Serializable]
    public class MaterialCreatorConfig : IMaterialCreatorConfig
    {
        [SerializeField] private MaterialByRank[] _addedMaterialsByRank;
        [SerializeField][Min(1f)] private float _accrualTime = 5f;

        public IEnumerable<IMaterialByRank> AddedMaterialsByRank => _addedMaterialsByRank;

        public float AccrualTime => _accrualTime;
    }
}