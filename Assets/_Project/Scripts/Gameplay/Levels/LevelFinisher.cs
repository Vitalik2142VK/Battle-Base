using BattleBase.Commands;
using BattleBase.Gameplay.Actors;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Levels
{
    public class LevelFinisher : MonoBehaviour
    {
        //todo a temporary solution
        [SerializeField] private CommandLoadMenuScene _command;

        private IWinStateController _winStateController;

        [Inject]
        public void Construct(IWinStateController winStateController)
        {
            _winStateController = winStateController ?? throw new ArgumentNullException(nameof(winStateController));
            _winStateController.BaseDestoyed += OnFinishLevel;
        }

        private void OnFinishLevel(Actor _)
        {
            _command.Execute();
        }
    }
}