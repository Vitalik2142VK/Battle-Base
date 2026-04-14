using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.Gameplay.Map;
using UnityEngine;

namespace BattleBase.Bootstraps
{
    public class MapBootstrap : BootstrapBase
    {
        [SerializeField] private List<CommandBase> _comandsToStart;
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _enemyColor;
        [SerializeField] private float _lightenFactor = 0.3f;
        [SerializeField] private List<Territory> _territories;
        [SerializeField] private Territory _playerStart;
        [SerializeField] private TerritoryConfig _config;

        private void Start()
        {
            foreach(CommandBase command in _comandsToStart)
                command.Execute();

            foreach (Territory territory in _territories)
                territory.Init(_enemyColor, TerritoryOwnerType.Enemy);

            _playerStart.SetColor(_playerColor);
            _playerStart.SetOwner(TerritoryOwnerType.Player);

            foreach (Territory territory in _playerStart.Adjacents)
            {
                if (territory.Owner != TerritoryOwnerType.Player)
                {
                    Color adjacentColor = Color.Lerp(_enemyColor, Color.white, _lightenFactor);
                    territory.SetColor(adjacentColor);
                    territory.SetOwner(TerritoryOwnerType.Adjacent);
                }
            }
        }
    }
}