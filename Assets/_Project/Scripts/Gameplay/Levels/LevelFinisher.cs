using System;
using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.Gameplay.Actors;
using BattleBase.SaveService;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Levels
{
    public class LevelFinisher : MonoBehaviour
    {
        //todo a temporary solution
        [SerializeField] private List<CommandBase> _finishCommands;

        private IWinStateController _winStateController;
        private ITerritorySaver _territorySaver;

        [Inject]
        public void Construct(IWinStateController winStateController, ITerritorySaver territorySaver)
        {
            _winStateController = winStateController ?? throw new ArgumentNullException(nameof(winStateController));
            _territorySaver = territorySaver ?? throw new ArgumentNullException(nameof(territorySaver));

            _winStateController.BaseDestoyed += OnFinishLevel;
        }

        private void OnFinishLevel(Actor _)
        {
            bool isWin = true;

            if (isWin)
            {
                ITerritoryData data = _territorySaver.TerritoryData;
                List<int> conqueredTerritories = new(data.ConqueredTerritories);
                int currentTerritoryIndex = data.SelectedTerrytory;

                if (conqueredTerritories.Contains(currentTerritoryIndex) == false)
                {
                    conqueredTerritories.Add(currentTerritoryIndex);
                    TerritoryData newData = new(conqueredTerritories);
                    _territorySaver.SetTerritoryData(newData);
                }
            }

            foreach (CommandBase command in _finishCommands)
                command.Execute();
        }
    }
}