using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Leaderboard
{
    [CreateAssetMenu(
        fileName = nameof(LeaderboardConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(LeaderboardConfig))]
    public class LeaderboardConfig : ScriptableObject
    {
        [SerializeField] private string _technoName;

        public string TechnoName => _technoName;
    }
}