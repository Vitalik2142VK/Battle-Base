using UnityEngine;

namespace BattleBase.Gameplay.Actors.Energy
{
    [System.Serializable]
    public class PowerByRank : IPowerByRank
    {
        [SerializeField][Min(1)] private int _addedPower = 10;

        public int AddedPower => _addedPower;
    }
}
