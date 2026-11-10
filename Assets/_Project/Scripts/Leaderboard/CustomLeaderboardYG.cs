using System;
using System.Collections.Generic;
using BattleBase.Leaderboard;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using YG;
using YG.Utils.LB;

namespace BattleBase
{
    public class CustomLeaderboardYG : MonoBehaviour
    {
        [Header("Leaderboard")]
        [SerializeField, Min(1)] private int _displayTopCount = 10;
        [SerializeField] private UpdateLeaderboardMethod _updateLBMethod = UpdateLeaderboardMethod.OnEnable;

        [Header("Players")]
        [SerializeField, Min(1)] private int _topPlayersCount = 3;
        [SerializeField, Min(0)] private int _playersBefore = 2;
        [SerializeField, Min(0)] private int _playersAfter = 2;

        [Header("Prefabs")]
        [SerializeField] private Transform _rootSpawnPlayersData;
        [SerializeField] private GameObject _playerDataPrefab;
        [SerializeField] private GameObject _separatorPrefab;

        [Header("Player Settings")]
        [SerializeField] private bool _loadPhotos = true;
        [SerializeField] private Sprite _hiddenPlayerPhoto;
        [SerializeField] private bool _timeTypeConvert;
        [SerializeField, Range(0, 3)] private int _decimalSize = 1;

        [SerializeField] private UnityEvent _onUpdateData;

        private LeaderboardHolder _leaderboardHolder;

        [Inject]
        public void Construct(LeaderboardHolder holder)
        {
            _leaderboardHolder = holder != null ? holder : throw new ArgumentNullException(nameof(holder));
        }

        private void OnEnable()
        {
            YG2.onGetLeaderboard += OnGetLeaderboard;

            if (_updateLBMethod == UpdateLeaderboardMethod.OnEnable)
                UpdateLB();
        }

        private void OnDisable()
        {
            YG2.onGetLeaderboard -= OnGetLeaderboard;
        }

        private void Start()
        {
            if (_updateLBMethod == UpdateLeaderboardMethod.Start)
                UpdateLB();
        }

        public void UpdateLB()
        {
            string photoSize = _loadPhotos ? "small" : "nonePhoto";
            int playersAround = GetPlayersAroundCount();

            YG2.GetLeaderboard(
                _leaderboardHolder.Leaderboard.TechnoName,
                _displayTopCount,
                playersAround,
                photoSize);
        }

        private void OnGetLeaderboard(LBData lbData)
        {
            if (lbData.technoName != _leaderboardHolder.Leaderboard.TechnoName)
                return;

            ClearList();

            if (lbData.entries == InfoYG.NO_DATA)
            {
                SpawnNoDataPlayer();
                _onUpdateData?.Invoke();

                return;
            }

#if UNITY_EDITOR
            lbData = LBMethods.SortLB(
                lbData,
                _displayTopCount + GetPlayersAroundCount(),
                _topPlayersCount,
                GetPlayersAroundCount());
#endif

            if (lbData.players == null || lbData.players.Length == 0)
            {
                _onUpdateData?.Invoke();

                return;
            }

            SpawnTopPlayers(lbData.players);

            int currentPlayerRank = GetCurrentPlayerRank(lbData);

            if (IsCurrentPlayerInTop(currentPlayerRank))
            {
                _onUpdateData?.Invoke();

                return;
            }

            List<LBPlayerData> playersAround = GetPlayersAroundCurrentPlayer(lbData.players, currentPlayerRank);

            if (playersAround.Count > 0)
            {
                SpawnSeparator();
                SpawnPlayers(playersAround);
            }

            _onUpdateData?.Invoke();
        }

        private void SpawnNoDataPlayer()
        {
            GameObject playerObject = Instantiate(_playerDataPrefab, _rootSpawnPlayersData);

            if (playerObject.TryGetComponent<LBPlayerDataYG>(out LBPlayerDataYG playerEntry) == false)
            {
                Debug.LogError(
                    "Player Data Prefab не содержит LBPlayerDataYG!",
                    playerObject);

                return;
            }

            string noData = string.Empty;

#if Localization_yg
            noData = YG2.lang switch
            {
                "ru" => "Нет данных",
                "en" => "No data",
                "tr" => "Veri yok",
                _ => string.Empty
            };
#endif

            playerEntry.data.name = noData;
            playerEntry.data.photoUrl = null;
            playerEntry.data.rank = null;
            playerEntry.data.score = null;
            playerEntry.data.inTop = false;
            playerEntry.data.currentPlayer = false;
            playerEntry.data.photoSprite = null;

            playerEntry.UpdateEntries();
        }

        private int GetPlayersAroundCount()
        {
            return Mathf.Clamp(
                _playersBefore + _playersAfter + 1,
                1,
                10);
        }

        private int GetCurrentPlayerRank(LBData lbData)
        {
            LBPlayerData currentPlayer = FindCurrentPlayer(lbData.players);

            if (currentPlayer != null)
                return currentPlayer.rank;

            if (lbData.currentPlayer != null)
                return lbData.currentPlayer.rank;

            return -1;
        }

        private LBPlayerData FindCurrentPlayer(LBPlayerData[] players)
        {
            foreach (LBPlayerData player in players)
            {
                if (player.uniqueID == YG2.player.id)
                    return player;
            }

            return null;
        }

        private bool IsCurrentPlayerInTop(int currentPlayerRank)
        {
            return currentPlayerRank > 0 &&
                currentPlayerRank <= _displayTopCount;
        }

        private void SpawnTopPlayers(LBPlayerData[] players)
        {
            List<LBPlayerData> topPlayers = GetTopPlayers(players);
            SpawnPlayers(topPlayers);
        }

        private List<LBPlayerData> GetTopPlayers(LBPlayerData[] players)
        {
            List<LBPlayerData> topPlayers = new();

            foreach (LBPlayerData player in players)
            {
                if (player.rank <= _displayTopCount)
                    topPlayers.Add(player);
            }

            SortPlayersByRank(topPlayers);

            return topPlayers;
        }

        private List<LBPlayerData> GetPlayersAroundCurrentPlayer(LBPlayerData[] players, int currentPlayerRank)
        {
            List<LBPlayerData> playersAround = new();

            if (currentPlayerRank <= _displayTopCount)
                return playersAround;

            int firstRank = Mathf.Max(_displayTopCount + 1, currentPlayerRank - _playersBefore);
            int lastRank = currentPlayerRank + _playersAfter;

            foreach (LBPlayerData player in players)
            {
                if (IsPlayerInRankRange(player, firstRank, lastRank))
                    playersAround.Add(player);
            }    

            SortPlayersByRank(playersAround);

            return playersAround;
        }

        private bool IsPlayerInRankRange(
            LBPlayerData player,
            int firstRank,
            int lastRank)
        {
            return player.rank >= firstRank &&
                   player.rank <= lastRank;
        }

        private void SortPlayersByRank(List<LBPlayerData> players) =>
            players.Sort((a, b) => a.rank.CompareTo(b.rank));

        private void SpawnPlayers(List<LBPlayerData> players)
        {
            foreach (LBPlayerData player in players)
                SpawnPlayer(player);
        }

        private void SpawnSeparator()
        {
            if (_separatorPrefab == null)
                return;

            Instantiate(_separatorPrefab, _rootSpawnPlayersData);
        }

        private void SpawnPlayer(LBPlayerData player)
        {
            GameObject playerObject = Instantiate(_playerDataPrefab, _rootSpawnPlayersData);

            if (playerObject.TryGetComponent(out LBPlayerDataYG playerEntry) == false)
            {
                Debug.LogError(
                    "Player Data Prefab не содержит LBPlayerDataYG!",
                    playerObject);

                return;
            }

            SetupPlayerEntry(playerEntry, player);
            playerEntry.UpdateEntries();
        }

        private void SetupPlayerEntry(LBPlayerDataYG playerEntry, LBPlayerData player)
        {
            playerEntry.data.name = LBMethods.AnonymousName(player.name);
            playerEntry.data.rank = player.rank.ToString();
            playerEntry.data.score = GetFormattedScore(player.score);
            playerEntry.data.inTop = player.rank <= _topPlayersCount;
            playerEntry.data.currentPlayer = IsCurrentPlayer(player);

            SetupPlayerPhoto(playerEntry, player);
        }

        private string GetFormattedScore(int score)
        {
            if (_timeTypeConvert)
                return LBMethods.TimeTypeConvertStatic(score, _decimalSize);

            return score.ToString();
        }

        private bool IsCurrentPlayer(LBPlayerData player) =>
            player.uniqueID == YG2.player.id;

        private void SetupPlayerPhoto(LBPlayerDataYG playerEntry, LBPlayerData player)
        {
            if (_loadPhotos == false)
                return;

            if (IsPlayerPhotoHidden(player))
            {
                playerEntry.data.photoSprite = _hiddenPlayerPhoto;

                return;
            }

            playerEntry.data.photoUrl = player.photo;
        }

        private bool IsPlayerPhotoHidden(LBPlayerData player)
        {
            if (_hiddenPlayerPhoto == null)
                return false;

            return player.photo == InfoYG.ANONYMOUS ||
                   (player.photo != null && player.photo.Contains("/avatar/0/"));
        }

        private void ClearList()
        {
            if (_rootSpawnPlayersData == null)
                return;

            for (int i = _rootSpawnPlayersData.childCount - 1; i >= 0; i--)
                Destroy(_rootSpawnPlayersData.GetChild(i).gameObject);
        }
    }
}