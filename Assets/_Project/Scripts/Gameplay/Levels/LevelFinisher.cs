using System;
using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.Gameplay.Map;
using BattleBase.ShopSystem;
using BattleBase.UI.PopUps;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Levels
{
    public class LevelFinisher : MonoBehaviour
    {
        private const float SecondsPerMinute = 60f;

        [SerializeField] private List<CommandBase> _finishCommands;
        [SerializeField] private WinPopUp _winPopUp;
        [SerializeField] private PopUp _losePopUp;
        [SerializeField] private float _fullAdditionalScoreInMinutes = 0f;
        [SerializeField] private float _zeroAdditionalScoreInMinutes = 30f;

        private IWinStateController _winStateController;
        private TerritoriesModel _territoriesModel;
        private CreditsModel _creditsModel;
        private float _startTime;

        [Inject]
        public void Construct(IWinStateController winStateController, TerritoriesModel territoriesModel, CreditsModel creditsModel)
        {
            _winStateController = winStateController ?? throw new ArgumentNullException(nameof(winStateController));
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));
            _creditsModel = creditsModel ?? throw new ArgumentNullException(nameof(creditsModel));            
        }

        private void Start() =>
            _startTime = Time.time;

        private void OnEnable() =>
            _winStateController.Winned += OnFinishLevel;

        private void OnDisable() =>
            _winStateController.Winned -= OnFinishLevel;

        private void OnFinishLevel(bool isWin)
        {
            foreach (CommandBase command in _finishCommands)
                command.Execute();

            if (isWin)
            {
                int index = _territoriesModel.Selected;
                bool isFirstWin = _territoriesModel.TryAddConqueredTerritory(index);
                int firstWinCredits = _territoriesModel.GetCreditsForFirstVictory(index);
                int basicCredits = isFirstWin ? firstWinCredits : 0;
                float elapsedSeconds = Time.time - _startTime;
                float timeInMinutes = elapsedSeconds / SecondsPerMinute;
                int additionalCredits = CalculateAdditionalCredits(firstWinCredits, timeInMinutes);

                _creditsModel.Increase(basicCredits + additionalCredits);
                _winPopUp.Show();
                _winPopUp.ShowCredits(basicCredits, additionalCredits);
            }
            else
            {
                _losePopUp.Show();
            }
        }

        private int CalculateAdditionalCredits(int basic, float timeMinutes)
        {
            if (_zeroAdditionalScoreInMinutes <= _fullAdditionalScoreInMinutes)
                return timeMinutes <= _fullAdditionalScoreInMinutes ? basic : 0;

            if (timeMinutes <= _fullAdditionalScoreInMinutes)
                return basic;

            if (timeMinutes >= _zeroAdditionalScoreInMinutes)
                return 0;

            float t = (timeMinutes - _fullAdditionalScoreInMinutes) / (_zeroAdditionalScoreInMinutes - _fullAdditionalScoreInMinutes);
            float bonus = basic * (1f - t);

            return Mathf.RoundToInt(bonus);
        }
    }
}