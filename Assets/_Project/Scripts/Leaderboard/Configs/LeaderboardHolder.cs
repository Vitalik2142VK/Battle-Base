using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Leaderboard
{
    [CreateAssetMenu(
        fileName = nameof(LeaderboardHolder),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(LeaderboardHolder))]
    public class LeaderboardHolder : ScriptableObject
    {
        [SerializeField] private LeaderboardConfig _leaderboard;

        public LeaderboardConfig Leaderboard => _leaderboard;
    }
}