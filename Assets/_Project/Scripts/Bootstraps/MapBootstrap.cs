using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.Gameplay.Map;
using BattleBase.Mediators;
using BattleBase.UI.PopUps;
using UnityEngine;

namespace BattleBase.Bootstraps
{
    public class MapBootstrap : BootstrapBase
    {
        [SerializeField] private List<CommandBase> _comandsToStart;
        [SerializeField] private ColorSettingsPopUp _colorSettingsPopUp;
        [SerializeField] private List<Territory> _territories;
        [SerializeField] private Territory _playerStart;
        [SerializeField] private MapColorMediator _colorMediator;

        private void Start()
        {
            foreach (CommandBase command in _comandsToStart)
                command.Execute();

            _colorSettingsPopUp.InitColors(2, 0);

            foreach (Territory territory in _territories)
                territory.Init(TerritoryOwnerType.Enemy);

            _playerStart.SetOwner(TerritoryOwnerType.Player);

            foreach (Territory territory in _playerStart.Adjacents)
            {
                if (territory.Owner != TerritoryOwnerType.Player)
                    territory.SetOwner(TerritoryOwnerType.Adjacent);
            }

            _colorMediator.Init();
        }
    }
}