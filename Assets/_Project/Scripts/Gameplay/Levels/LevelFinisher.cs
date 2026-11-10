using System;
using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using BattleBase.Leaderboard;
using BattleBase.ShopSystem;
using BattleBase.UI.PopUps;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Levels
{
    public class LevelFinisher : MonoBehaviour, IInjectable
    {
        private const float SecondsPerMinute = 60f;

        [SerializeField] private List<CommandBase> _finishCommands;
        [SerializeField] private WinPopUp _winPopUp;
        [SerializeField] private PopUp _losePopUp;
        [SerializeField][Min(0f)] private float _fullRewardTimeThresholdInMinutes = 0f;
        [SerializeField][Min(0f)] private float _minimumRewardTimeThresholdInMinutes = 60f;
        [SerializeField][Range(0f, 1f)] private float _minimumRewardCoefficient = 0.1f;

        private ILeaderboardScoreService _leaderboardScoreService;
        private IWinStateController _winStateController;
        private IScoreCalculator _scoreCalculator;
        private TerritoriesModel _territoriesModel;
        private CreditsModel _creditsModel;
        private float _levelStartTime;

        [Inject]
        public void Construct(
            ILeaderboardScoreService leaderboardScoreService,
            IWinStateController winStateController,
            IScoreCalculator scoreCalculator,
            TerritoriesModel territoriesModel,
            CreditsModel creditsModel)
        {
            _leaderboardScoreService = leaderboardScoreService ?? throw new ArgumentNullException(nameof(leaderboardScoreService));
            _winStateController = winStateController ?? throw new ArgumentNullException(nameof(winStateController));
            _scoreCalculator = scoreCalculator ?? throw new ArgumentNullException(nameof(scoreCalculator));
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));
            _creditsModel = creditsModel ?? throw new ArgumentNullException(nameof(creditsModel));
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_minimumRewardTimeThresholdInMinutes <= _fullRewardTimeThresholdInMinutes)
            {
                Debug.LogWarning(
                    $"{nameof(_minimumRewardTimeThresholdInMinutes)} должен быть больше {nameof(_fullRewardTimeThresholdInMinutes)} — " +
                    $"иначе time-based награда работает в бинарном режиме (полная/ноль), " +
                    $"{nameof(_minimumRewardCoefficient)} в нём НЕ применяется.",
                    this);
            }
        }
#endif

        private void Start() =>
            _levelStartTime = Time.time;

        private void OnEnable()
        {
            _winStateController.Winned += OnFinishLevel;
            _winPopUp.RewardIncreased += OnRewardIncreased;
        }

        private void OnDisable()
        {
            _winStateController.Winned -= OnFinishLevel;
            _winPopUp.RewardIncreased -= OnRewardIncreased;
        }

        private void OnFinishLevel(bool hasPlayerWon)
        {
            foreach (CommandBase command in _finishCommands)
                command.Execute();

            if (hasPlayerWon)
            {
                int territoryIndex = _territoriesModel.SelectedTerritoryIndex;
                bool isFirstWin = _territoriesModel.TryMarkTerritoryConquered(territoryIndex);
                int creditsForFirstVictory = _territoriesModel.GetCreditsForFirstVictory(territoryIndex);
                int baseCredits = isFirstWin ? creditsForFirstVictory : 0;
                float elapsedSeconds = Time.time - _levelStartTime;
                float elapsedMinutes = elapsedSeconds / SecondsPerMinute;
                int additionalCredits = CalculateTimeBasedAdditionalCredits(creditsForFirstVictory, elapsedMinutes);

                //todo temporarily
                int leaderboardScore = _scoreCalculator.Calculate();
                _leaderboardScoreService.AddScore(leaderboardScore);

                _creditsModel.Increase(baseCredits + additionalCredits);
                _winPopUp.Show();
                _winPopUp.ShowCredits(baseCredits, additionalCredits);
            }
            else
            {
                _losePopUp.Show();
            }
        }

        private void OnRewardIncreased(int creditsToAdd) =>
            _creditsModel.Increase(creditsToAdd);

        private int CalculateTimeBasedAdditionalCredits(int maximumAdditionalCredits, float elapsedMinutes)
        {
            if (_minimumRewardTimeThresholdInMinutes <= _fullRewardTimeThresholdInMinutes)
                return elapsedMinutes <= _fullRewardTimeThresholdInMinutes ? maximumAdditionalCredits : 0;

            if (elapsedMinutes <= _fullRewardTimeThresholdInMinutes)
                return maximumAdditionalCredits;

            if (elapsedMinutes >= _minimumRewardTimeThresholdInMinutes)
                return Mathf.RoundToInt(maximumAdditionalCredits * (_minimumRewardCoefficient));

            float normalizedTime = (elapsedMinutes - _fullRewardTimeThresholdInMinutes) /
                                   (_minimumRewardTimeThresholdInMinutes - _fullRewardTimeThresholdInMinutes);

            float currentRewardCoefficient = Mathf.Lerp(1f, _minimumRewardCoefficient, normalizedTime);

            return Mathf.RoundToInt(maximumAdditionalCredits * (currentRewardCoefficient));
        }
    }
}