using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    [System.Serializable]
    public class MaterialByRank : IMaterialByRank
    {
        [SerializeField][Min(1)] private int _addedMaterials = 20;

        public int AddedMaterials => _addedMaterials;
    }
}