using System;
using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Levels
{
    public class LevelFinisher : MonoBehaviour
    {
        [SerializeField] private List<CommandBase> _finishCommands;

        private IWinStateController _winStateController;
        private TerritoriesModel _territoriesModel;

        [Inject]
        public void Construct(IWinStateController winStateController, TerritoriesModel territoriesModel)
        {
            _winStateController = winStateController ?? throw new ArgumentNullException(nameof(winStateController));
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));

            _winStateController.Winned += OnFinishLevel;
        }

        private void OnFinishLevel(bool isWin)
        {
            if (isWin)
                _territoriesModel.AddConqueredTerritory(_territoriesModel.Selected);

            foreach (CommandBase command in _finishCommands)
                command.Execute();
        }
    }
}