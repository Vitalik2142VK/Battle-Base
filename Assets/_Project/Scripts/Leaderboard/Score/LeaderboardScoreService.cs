using System;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace BattleBase.Leaderboard
{
    public class LeaderboardScoreService : ILeaderboardScoreService, IDisposable
    {
        private readonly LeaderboardHolder _holder;

        private bool _isUpdateInProgress;
        private int _pendingScore;

        public LeaderboardScoreService(LeaderboardHolder holder)
        {
            _holder = holder != null ? holder : throw new ArgumentNullException(nameof(holder));
        }

        public void AddScore(int score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score), score, "Score must be non-negative");

            if (_holder.Leaderboard == null)
            {
                Debug.LogError($"{nameof(LeaderboardHolder)} has no assigned {nameof(LeaderboardConfig)}.", _holder);

                return;
            }

            if (_isUpdateInProgress)
            {
                Debug.LogWarning("Leaderboard score update is already in progress, request skipped.");

                return;
            }

            _isUpdateInProgress = true;
            _pendingScore = score;

            YG2.onGetLeaderboard += OnLeaderboardReceived;
            YG2.GetLeaderboard(_holder.Leaderboard.TechnoName, 1, 1, "nonePhoto");
        }

        public void Dispose() =>
            YG2.onGetLeaderboard -= OnLeaderboardReceived;

        private void OnLeaderboardReceived(LBData lbData)
        {
            if (_holder.Leaderboard == null)
            {
                YG2.onGetLeaderboard -= OnLeaderboardReceived;

                return;
            }

            if (lbData.technoName != _holder.Leaderboard.TechnoName)
                return;

            YG2.onGetLeaderboard -= OnLeaderboardReceived;

            int currentScore = lbData.currentPlayer?.score ?? 0;
            int newTotalScore = currentScore + _pendingScore;

            YG2.SetLeaderboard(_holder.Leaderboard.TechnoName, newTotalScore);

            _pendingScore = 0;
            _isUpdateInProgress = false;
        }
    }
}